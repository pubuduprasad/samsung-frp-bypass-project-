echo Waiting for device.
adb wait-for-device
echo Unlocking Device
adb push frp.bin /data/local/tmp/temp > NUL
adb shell chmod 777 /data/local/tmp/temp > NUL
adb shell /data/local/tmp/temp
adb shell rm /data/local/tmp/temp > NUL
pause