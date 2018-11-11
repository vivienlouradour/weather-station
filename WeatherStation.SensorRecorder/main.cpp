#include <iostream>
#include <string>
#include "I2cReader.h"
#include "Api.h"
#include "utils.h"
#include <sstream>
using namespace std;



int main(){
    Api api("host", 25, "pine64");
    api.print();
    stringstream stream;

    char currentDate[256];
    
    
    I2cReader i2cReader;
    try{
        //Get records
        float hum = i2cReader.humidity();
        float temp = i2cReader.temperature();
        
        //Log records
        stream << "Humidity : " << hum;
        log(stream.str());
        
        stream << "Temperature : " << temp;
        log(stream.str());
    }
    catch(const exception&){
        return EXIT_FAILURE;
    }
}

