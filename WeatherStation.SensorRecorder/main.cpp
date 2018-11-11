#include <string>
#include <sstream>
#include "I2cReader.h"
#include "Api.h"
#include "utils.h"


using namespace std;

int main(int argc, char** argv){
    //Get main args
    if(argc < 4){
        log("3 parameters required : host, port and broadcaster name");
        return EXIT_FAILURE;
    }
    string host = string(argv[1]);
    int port = stoi(argv[2]);
    string broadcasterName = string(argv[3]);

    Api api(host, port, broadcasterName);
    api.print();

    stringstream stream;  
    I2cReader i2cReader;
    char date[30];
    try{
        //Get record
        float hum = i2cReader.humidity();
        float temp = i2cReader.temperature();
        
        //Log record
        stream << "Humidity : " << hum;
        log(stream.str());
        
        stream.str(""); // clear the stringstream (set to "")
        stream << "Temperature : " << temp;
        log(stream.str());

        //Send record
        writeFormatedCurrentDate(date);
        api.sendRecord(temp, hum, date);
    }
    catch(const exception&){
        return EXIT_FAILURE;
    }
}

