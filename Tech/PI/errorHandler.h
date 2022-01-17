#ifndef ERRORHANDLER_H
#define ERRORHANDLER_H



class errorHandler
{
	public:
		errorHandler();
		virtual ~errorHandler();
	
		char* extraInfo;
		const char* resolveError(int e);
		void throwFatal(int e);
		void throwWarn(int e, bool logStyle=false);
};

#endif //ERRORHANDLER_H
