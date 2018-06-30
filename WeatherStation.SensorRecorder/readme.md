# Weather station - Sensor recorder

# Pine64 setup : 
- Connect the SI7021 sensor
- To enable i2c bus on pine64, add these lines to `/boot/config.txt` :  
```
dtparam=i2c1=on  
dtparam=i2c=on  
dtparam=i2c_arm=on  
dtparam=i2c_vc  
device_tree_param=i2c=on1=on  
dtparam=spi=on   
```
- Reboot : `sudo reboot`

