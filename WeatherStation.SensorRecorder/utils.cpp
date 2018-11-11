#include "utils.h"

using namespace std;

char currentDateTime[30];

void logError(std::string message){
    writeFormatedCurrentDate(currentDateTime);
    cout << currentDateTime << " - [ERROR] : " << message << endl;
}

void logInfo(std::string message){
    writeFormatedCurrentDate(currentDateTime);
    cout << currentDateTime << " - [INFO] : " << message << endl;
}

/**
 * Write current dateTime in char* parameter
 * Formated to suit API request
 */
void writeFormatedCurrentDate(char* date){
    time_t t = time(NULL);
    struct tm tm = *localtime(&t);

    sprintf(date, "%d-%02d-%02dT%02d:%02d:%02d.000Z", tm.tm_year + 1900, tm.tm_mon + 1, tm.tm_mday, tm.tm_hour, tm.tm_min, tm.tm_sec);
}

