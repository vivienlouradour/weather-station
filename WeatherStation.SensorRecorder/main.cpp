#include <string>
#include <sstream>
#include "I2cReader.h"
#include "Api.h"
#include "utils.h"


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
        
        stream.str(""); // clear the stringstream (set to "")
        stream << "Temperature : " << temp;
        log(stream.str());


    }
    catch(const exception&){
        return EXIT_FAILURE;
    }
}

