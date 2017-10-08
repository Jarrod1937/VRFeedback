#include <string.h>
#include <stdlib.h>


// Serial string buffer
String inData;

// Pin assignments
int outpins[] = {4, 5, 6, 7};


void setup()
{
    Serial.begin(9600);
    setoutput();
    sethigh();
}


void loop()
{
  serialprocess();
}

void setoutput()
{
  
  int ix;
  int arrsize = (sizeof(outpins)/sizeof(int));
  
  // Sets all pins in array to OUTPUT
  for(ix=0; ix < arrsize; ++ix)
  {
    pinMode(outpins[ix], OUTPUT);
  }
  
}

void sethigh()
{
  
  int ix;
  int arrsize = (sizeof(outpins)/sizeof(int));
  
  // Resets all pins in array to HIGH state, relay is activated by a low state
  for(ix=0; ix < arrsize; ++ix)
  {
   digitalWrite(outpins[ix], HIGH);
  }
  
}

void serialprocess()
{
  if (Serial.available() > 0)
  {
    int byteInput = Serial.read();

    // Convert byte to int's based on first and last 4 bits
    int bitnum1 = byteInput & 0xF;
    int bitnum2 = (byteInput >> 4) & 0xF;
    //int bitnum1 = byteInput & 0x0F;
    //int bitnum2 = byteInput >> 3;

    int ix;
    int arrsize = (sizeof(outpins)/sizeof(int));

    // If referenced port is too high, set to 0
    if(bitnum2 > (arrsize - 1))
    {
        bitnum1 = 0;
    }
    Serial.print(outpins[bitnum2], BIN);
    digitalWrite(outpins[bitnum2], LOW);
    delay(50*bitnum1);
    digitalWrite(outpins[bitnum2], HIGH);
  }
}
