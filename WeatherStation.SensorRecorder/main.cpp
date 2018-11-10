#include <iostream>
#include <stdio.h>
#include <string>
#include "I2cReader.h"

using namespace std;

/**
 * Write current dateTime in char* parameter
 * Formated to suit API request
 */
void writeFormatedCurrentDate(char* date){
    time_t t = time(NULL);
    struct tm tm = *localtime(&t);

    sprintf(date, "%d-%02d-%02dT%02d:%02d:%02d.000Z", tm.tm_year + 1900, tm.tm_mon + 1, tm.tm_mday, tm.tm_hour, tm.tm_min, tm.tm_sec);
}

int main(){
   char currentDate[256];
    cout << "hello world! "<< endl;
    writeFormatedCurrentDate(currentDate);
    cout << currentDate << endl;
    I2cReader i2cReader;
    try{
        float hum = i2cReader.humidity();
        float temp = i2cReader.temperature();
        cout << "Humidity : " << hum << endl;
        cout << "Temperature : " << temp << endl;
    }
    catch(const exception&){
        return EXIT_FAILURE;
    }
}

