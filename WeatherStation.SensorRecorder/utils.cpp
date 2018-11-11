#include "utils.h"

using namespace std;

char currentDateTime[30];

void log(std::string message){
    writeFormatedCurrentDate(currentDateTime);
    cout << "[" << currentDateTime << "] : " << message << endl;
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

