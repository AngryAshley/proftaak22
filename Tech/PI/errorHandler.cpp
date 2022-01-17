#include "errorHandler.h"
#include <stdio.h>
#include <stdlib.h>
#include <string.h>

errorHandler::errorHandler(){
	extraInfo=strdup("");
}
errorHandler::~errorHandler(){}

const char* errorHandler::resolveError(int e){
	switch(e){
		case 0: return "OK";
		case 1: return "GENERIC ERROR";
		case 200: return "Generic OpenCV error";
		case 201: return "Could not initialise OpenCV";
		case 202: return "Aborting OpenCV initialisation";
		case 203: return "OpenCV";//for cv::Exception messages
		case 204: return "Could not initialise camera";
		case 220: return "Empty frame, camera inaccessible.";
		case 300: return "Generic SSL error";
		case 301: return "Could not initialise SSL Socket";
		case 302: return "Aborting SSL Client initialisation";
		case 303: return "Could not initialise SSL Client, error opening socket";
		case 304: return "SSL Hostname is null";
		case 305: return "Server returned no SSL Certificate";
		case 306: return "SSL socket failed to connect";
		case 307: return "SSL address incorrect!";
		case 308: return "SSL not initialized!";
		case 309: return "SSL pointer is empty!";
		case 310: return "SFD has returned an error!";
		case 900: return "Generic debug error";
		case 901: return "Program reached undesirable area";
		default: return "UNDEFINED ERROR";
	}
	return "";
}

void errorHandler::throwFatal(int e){
	printf("\r\n\r\n\033[1;31m *** FATAL ERROR %d, PROGRAM EXITED. REASON: %s %s\n\n\033[0m",e,resolveError(e),extraInfo);
	exit(e);
}

void errorHandler::throwWarn(int e, bool logStyle){
	if(logStyle){printf("\033[80D[\033[1;38;5;208mWARN\033[0m");}
	printf("\n\033[1;38;5;208m - ERROR %d: %s %s\n\r\033[0m",e,resolveError(e),extraInfo);
} 
