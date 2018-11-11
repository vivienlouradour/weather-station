#include <string>
#include <sstream>
#include "I2cReader.h"
#include "Api.h"
#include "utils.h"

int recordDelay = 600; //Time (in seconds) beetween to records

using namespace std;

int main(int argc, char** argv){
    //Get main args
    if(argc < 4){
        logError("Invalid parameters : 3 required : (host, port, broadcaster name)");
        return EXIT_FAILURE;
    }

    string host = string(argv[1]);
    int port = atoi(argv[2]);
    string broadcasterName = string(argv[3]);

    Api api(host, port, broadcasterName);

    stringstream stream;  
    I2cReader i2cReader;
    string date;
    try{
        float humidity;
        float temperature;

        while(1){
            //Get record
            humidity = i2cReader.humidity();
            temperature = i2cReader.temperature();
            
            //Log record
            stream << "Humidity : " << humidity;
            logInfo(stream.str());
            
            stream.str(""); // clear the stringstream (set to "")
            stream << "Temperature : " << temperature;
            logInfo(stream.str());

            //Send record
            date = getCurrentDate();
            api.sendRecord(temperature, humidity, date);

            stream.str("");
            //Wait before next record
            stream << "Sleeping for " << recordDelay << " seconds before next record...";
            logInfo(stream.str());
            sleep(recordDelay);
        }
    }
    catch(const exception&){
        return EXIT_FAILURE;
    }
}

