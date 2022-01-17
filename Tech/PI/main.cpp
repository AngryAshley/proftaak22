#include <stdio.h>
#include <stdlib.h>
#include <string.h>
#include <unistd.h>
#include <sys/select.h>
#include <termios.h>
#include <time.h>

#include <iostream>


#include <opencv2/opencv.hpp>
#include <opencv2/highgui.hpp>
#include <opencv2/imgproc.hpp>
#include <opencv2/imgcodecs.hpp>
#include "errorHandler.h"

#include <errno.h>
//#include <resolve.h>
#include <netdb.h>
#include <openssl/ssl.h>
#include <openssl/err.h>

using namespace std;
using namespace cv;

errorHandler err;
VideoCapture cap;
HOGDescriptor hog;

string FTPuser= "student";
string FTPpass= "TopMaster99";

uint8_t ver = 1;
uint8_t rev = 0;
bool running=true;
Mat frame,video;
bool gui=false;
char imageTag[146];
int camID = 1;

int framecount=0;

//opencv tracking
vector<Point> track;
vector<Rect> found;
vector<double> weights;
bool tracking;
time_t rawtime;
int conEvents;
double maxConf=0.0;


//SSL
//int sockfd, newsockfd;
SSL_CTX *sslctx;
SSL *ssl, *ssl2;
char* aPortNumber = (char*)"6023";
char* serv_addr = (char*)"192.168.161.205";
char* sendMsg = (char*)"ERROR!";
int sfd;


const int ERROR_STATUS = -1;

struct termios orig_termios;

void reset_terminal_mode(){tcsetattr(0,TCSANOW,&orig_termios);}
void set_conio_terminal_mode(){
	struct termios new_termios;
	tcgetattr(0, &orig_termios);
	memcpy(&new_termios, &orig_termios, sizeof(new_termios));
	atexit(reset_terminal_mode);
	cfmakeraw(&new_termios);
	tcsetattr(0,TCSANOW, &new_termios);
}
int kbhit(){
	struct timeval tv = {0L, 0L};
	fd_set fds;
	FD_ZERO(&fds);
	FD_SET(0,&fds);
	return (select(1,&fds,NULL,NULL, &tv) >0);
}
int getch(){
	int r;
	unsigned char c;
	if((r=read(0,&c,sizeof(c)))<0){
		return r;
	}else{
		return c;
	}
}


SSL_CTX *InitSSL_CTX(void){
	const SSL_METHOD *method = TLS_client_method();
	SSL_CTX *ctx = SSL_CTX_new(method);
	if(ctx==nullptr)err.throwFatal(301);
	return ctx;
}

int OpenConnection(const char *hostname, const char *port){
	struct hostent *host;
	if((host=gethostbyname(hostname))==nullptr)err.throwFatal(304);
	struct addrinfo hints = {0}, *addrs;
	hints.ai_family = AF_INET; // or AF_UNSPEC
	hints.ai_socktype = SOCK_STREAM;
	hints.ai_protocol = IPPROTO_TCP;
	
	const int status = getaddrinfo(hostname,port,&hints,&addrs);
	if(status!=0)err.throwFatal(307);
	
	int errnum;
	for (struct addrinfo *addr = addrs; addr!=nullptr; addr=addr->ai_next){
		sfd = socket(addrs->ai_family, addrs->ai_socktype, addrs->ai_protocol);
		if(sfd==ERROR_STATUS){errnum=errno;continue;}
		if(connect(sfd,addr->ai_addr, addr->ai_addrlen)==0)break;
		errnum = errno;
		sfd = ERROR_STATUS;
		printf(" SFD ERROR %d ",errnum);
		close(sfd);
	}
	freeaddrinfo(addrs);
	
	if(sfd==ERROR_STATUS)err.throwFatal(300);
	return sfd;
}

void DisplayCerts(SSL *sslint){
	X509 *cert = SSL_get_peer_certificate(sslint);
	if(cert!=nullptr){
		printf("Server certificates:\r\n");
		char *line = X509_NAME_oneline(X509_get_subject_name(cert),0,0);
		printf("Subject %s\r\n",line);
		delete line;
		line= X509_NAME_oneline(X509_get_issuer_name(cert),0,0);
		printf("Issuer %s\r\n",line);
		delete line;
		X509_free(cert);
	} else {
		err.throwFatal(305);
	}
}



void error(char *msg){ //for syscall error handling.
	perror(msg);
	exit(1);
}

void stop(){
	printf("Performing safe shutdown...\r\n");
	SSL_free(ssl);
	close(sfd);
	SSL_CTX_free(sslctx);
	reset_terminal_mode();
	printf("Stopped successfully!\r\n");
	exit(0);
}

void sendSSLmsg(SSL *sslint, string msg){
	if(sslint==nullptr)err.throwFatal(309);
	SSL_write(sslint,msg.c_str(),msg.length());
}

void init(){
	printf("       Initialising...\r\n");
	
	printf("[....] Initialising OpenCV V%s",CV_VERSION);
	try{
		if(NULL==getenv("DISPLAY")) gui=false;
		else gui=true;
		
		cap.open(0,CAP_ANY);
		if(!cap.isOpened())throw 204;
		
		if(gui){
			printf(" - Running headed");
			cv::namedWindow("RailView - Live", cv::WINDOW_NORMAL);
		} else {
			printf(" - Running headless");
		}
		hog.setSVMDetector(HOGDescriptor::getDefaultPeopleDetector());
		
		printf("\033[80D[\033[1;32m OK \033[0m]\r\n");
	}catch(const cv::Exception& e){
		err.extraInfo=strdup(e.what());
		throw 203;
	}catch(const int e){
		if(e!=202){
			throw e;
		} else {
			err.throwWarn(e,true);
		}
	}
	
	/*
	try{
		
	}catch(int e){
		if(e!=302)throw e;
		err.throwWarn(e,true);
	}
	*/
	printf("[....] Initialising SSL Socket");
		//throw 302; //bypass SSL
		sslctx = InitSSL_CTX();
		ssl = SSL_new(sslctx);
		if(ssl==nullptr)throw 301;
		
		sfd = OpenConnection(serv_addr,aPortNumber);
		SSL_set_fd(ssl,sfd);
		
		const int status = SSL_connect(ssl);
		if(status!=1){
			SSL_get_error(ssl,status);
			ERR_print_errors_fp(stderr);
			
			fprintf(stderr, "SSL_CONNECT FAILED W CODE %d",status);
			throw 306;
		}

		sendSSLmsg(ssl,"LOGGED ON\n");
		if(ssl==nullptr)throw 309;
		printf("\033[80D[\033[1;32m OK \033[0m\r\n");
} 



int main(void){
	set_conio_terminal_mode();
	printf(" *** RailView V%d.%d *** \r\n",ver,rev);
	
	try{
	    init();
	    if(ssl==nullptr)throw 309;
	}catch(int e){
		err.throwFatal(e);
	}
	printf("       Initialisation successful!\r\n");
    printf(" Press E to exit program\r\n");
    char key;
    
    while(running){
		char timeBuf[80];
		time(&rawtime);
		strftime(timeBuf,sizeof(timeBuf),"%d-%m-%Y %H:%M:%S",localtime(&rawtime));
			
		cap.read(frame);
		if(frame.empty())err.throwFatal(220);
		video = frame.clone();
		resize(frame,frame,Size(frame.cols/2,frame.rows/2));
		rotate(frame,frame,ROTATE_90_COUNTERCLOCKWISE);
		
		//feature detection
		hog.detectMultiScale(frame,found,weights);
		
		for(size_t i=0; i< found.size(); i++){
			//Rect r=found[i];
			rectangle(frame,found[i], cv::Scalar(0,0,255),3);
			//stringstream temp;
			//temp << weights[i];
			//putText(frame, temp.str(), Point(found[i].x,found[i].y+50), FONT_HERSHEY_SIMPLEX,1,Scalar(0,0,255));
			
			if(weights[i]>maxConf)maxConf=weights[i];
			printf("%s%s>Detection event raised, Conf:%f, Consecutive: %d\033[0m\r\n",conEvents>=5?"\033[1;31m":"",timeBuf,weights[i],conEvents+1);
			track.push_back(Point(found[i].x+found[i].width/2,found[i].y+found[i].height/2));
		}
		
		if(found.size()==0){
			track.clear();
			conEvents=0;
			if(tracking)printf("%s> Lost tracking\r\n",timeBuf);
			tracking=false;
		}else{
			tracking=true;		
			if(maxConf>=0.5)conEvents++;
			
		}
		
		if(conEvents==5){
			printf("%s>\033[1;31mPERSON DETECTED\033[0m\r\n",timeBuf);
			
			
		}
		if(conEvents>=5){
			sendSSLmsg(ssl,"PERSON\n");
		}
		
		
		/*for(size_t i=1; i< track.size(); i++){
			line(frame,track[i-1],track[i],Scalar(255,255,0),2);
		}
		*/
		
		if(gui){
			imshow("RailView - Live",frame);
			cv::waitKey(10);
		}
		
		if(framecount==60){
			sprintf(imageTag,"%06d.jpg",camID);
			imwrite(imageTag,frame);
			system("curl -T \"./000001.jpg\" \"sftp://192.168.161.205:21\" --trace ftptrace.txt --user student:TopMaster99");

			framecount=0;
		} else {
			framecount++;
		}
		if(kbhit()){
			key=getch();
			if(key!=0){
				if(key=='e'){running=false;}
				key=0;
			}
		}
	}
	
	stop();
	err.throwFatal(901);
	return 0;
}
