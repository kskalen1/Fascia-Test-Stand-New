/*****************************************************************************

  COMPANY:                Autoliv
  
  CONFIGURATION ITEM:     MXBET

  CPU:                    -

  COMPONENT:              -

  MODULE:                 -

  FILENAME:               mxbet.h

  DESCRIPTION:            -

  ORIGINATOR:             Markus Rieser

  DATE OF CREATION:       November 19, 2010

  CHANGES
  ---------------

  ARCHIVENAME:           $Source: /var/cvs/linuxtools/mxbet/src/mxbet.h,v $

  VERSIONNUMBER:         $Revision: 1.6 $

  EDITOR NAME:           $Author: mrieser $

  DATE OF LAST EDIT:     $Date: 2013/01/25 12:30:36 $

  CHANGE:                $Log: mxbet.h,v $
  CHANGE:                Revision 1.6  2013/01/25 12:30:36  mrieser
  CHANGE:                - added function BET_GetSensorVerExt to read Subversion of sensor software
  CHANGE:                - bugfix setting sensor mode in Sync message
  CHANGE:
  CHANGE:                Revision 1.5  2012/07/13 21:30:58  mrieser
  CHANGE:                2_4_0 in head
  CHANGE:
  CHANGE:                Revision 1.1.1.1.8.4  2012/07/11 12:21:11  mrieser
  CHANGE:                - changed version to 2.4.0
  CHANGE:                - Added Retracing function for Bumper specific data and written DTR Offset information
  CHANGE:                - added XCP on before EEPROM read in GetTotalConnected Device function and check EEPROM maximum 3 times for secure read of E²PROM
  CHANGE:                - added Re-Reading of DTR Offset values for Retracing
  CHANGE:                - added flag for detecting if BET_SetBumperType was called correctly and the retracing function returns valid values
  CHANGE:                - removed check of last occurance time for EEPROM errors -> all errors except of error 10 will cause an error of the testbench
  CHANGE:                - startup time (boot time) of sensor will be taken from cfg file again
  CHANGE:                mxtestbench:
  CHANGE:                added retracing function call and display Retraced values in textbox
  CHANGE:
  CHANGE:                Revision 1.1.1.1.8.3  2012/06/29 10:25:53  mrieser
  CHANGE:                - changed Version to 2.3.4
  CHANGE:                - added Platform specific data and verification with new MXBET Error(20) "ERR_PLATFORM_PARAM_NOT_VALID"
  CHANGE:                - added additional log information
  CHANGE:                - added delay of sensor test until testtime reaches 11 seconds minimum before error check
  CHANGE:                - added new dll function "BET_SetBumperType
  CHANGE:
  CHANGE:                Revision 1.1.1.1.8.2  2012/05/07 20:23:33  mrieser
  CHANGE:                - changed Version to 2.3.3
  CHANGE:                - added sensor errors for writing sensor offset and platform check failed
  CHANGE:                - added dll error check in WriteSensorOffset function
  CHANGE:                - added check for sensor error removal in ErrorCheck
  CHANGE:                - added additional Logfile information
  CHANGE:                - added check for minimum test time before doing error check
  CHANGE:
  CHANGE:                Revision 1.1.1.1.8.1  2012/04/11 16:29:31  mrieser
  CHANGE:                - changed version to 2_3_0
  CHANGE:                - added possibility to add EEPROM to Log File
  CHANGE:                - added functionality to write DTR Offset (only Table 2 offset) is written
  CHANGE:                - added cfg parameter for DTR Offset and EEPROM Log
  CHANGE:                - changed order of clutter test and sensor error test
  CHANGE:                - added a delay for sensor error check if test time is less than 10 sec -> provides setting of error codes in EEPROM
  CHANGE:
  CHANGE:                Revision 1.1.1.1  2012/03/05 09:14:23  dhavlik
  CHANGE:                no message
  CHANGE:

The information contained in this document or item is the property of Autoliv
Inc.,and/or its subsidiaries hereinafter collectively ("Autoliv"), and shall be 
kept in strict confidence. Except with the written permission of Autoliv, such 
information or item shall not be published, or disclosed to others, or used for 
manufacture or sale or for any other purpose, and this document or item shall 
not be reproduced, in whole or in part. If permission is granted for reproduction,
this legend shall be included in any such reproduction. This document or item
shall be returned to Autoliv upon request, or completion of the use for which
it was made available to recipient, or termination of relationship with 
recipient, whichever first occurs. Any recipient so agrees by acceptance of 
this document or item. Copyright 2010, Autoliv, Unpublished. 
All rights under the copyright laws of the United States and other laws.
*****************************************************************************/

#if !defined MXBET_H
#define MXBET_H

// The following ifdef block is the standard way of creating macros which make exporting
// from a DLL simpler. All files within this DLL are compiled with the BET_EXPORTS
// symbol defined on the command line. this symbol should not be defined on any project
// that uses this DLL. This way any other project whose source files include this file see
// BET_API functions as being imported from a DLL, whereas this DLL sees symbols
// defined with this macro as being exported.
#ifdef WIN32
  #ifdef BET_EXPORTS
    #define BET_API __declspec(dllexport)
  #else
    #define BET_API __declspec(dllimport)
  #endif
    
 #define BET_MIDDECL   __stdcall   
#else
  #define BET_API
  #define BET_MIDDECL  
#endif

#include <string>

using namespace std;

#define SEN_ERR_MASK 0x00FFFFFF
#define MAKE_ERRORCODE(m,e) (((m)<<24)|(e))
#define GET_ERROR(e) (SEN_ERR_MASK&(e))
#define GET_SENSMASK(e) ((e) >> 24)

#define ERR_NO_ERROR                   0     //Success
#define ERR_CAN_HARDWARE               1     //CAN-Hardware not valid
#define ERR_PLATFORM_NOT_VALID		   2     //Platform ID not valid
#define ERR_BUMPER_NOT_VALID		      3      //Bumper not valid
#define ERR_INVALID_SENSOR_PLACEMENT   4     //placement of the sensors differs from configuration
#define ERR_SENSOR_NO_ANSWER           5     //Sensor did not respond to ping
#define ERR_SENSOR_BAD_VERSION         6     //Sensor version differs from configuration
#define ERR_SENSOR_NOT_PROG            7     //Sensor has not been programmed
#define ERR_EEPROM_READ                8     
#define ERR_NO_SENSOR_DETECTED         9     //no sensor detected on the bus

#define ERR_VOLTAGE                    10
#define ERR_HARDWARE_FAILURE		      11
#define ERR_ADDRESS_UNSTABLE		      12
#define ERR_INTERNAL_NOISE             13
#define ERR_HARDWARE_FAILURE_ONLINE	   16

#define ERR_SW1				            14     //Software error 1
#define ERR_SW2				            15	    //Software error 2
#define ERR_CLUTTER_TEST               17     //Software error 3
#define ERR_CFG_NOT_SEALED             18     //CFG file not sealed
#define ERR_WRONG_OP_MODE	            19     //Sensor has wrong operational mode
#define ERR_PLATFORM_PARAM_NOT_VALID   20     //Platform Parameter/ID not valid
#define ERR_WRITE_OFFSET               21     //Sensor offset could not be written to EEPROM

#define BET_MAX_ERRORS      (ERR_WRITE_OFFSET)

#define FRONT_BUMPER 1
#define REAR_BUMPER 2
#define MAX_BUMPER_SENSORS (4)

#define SENSOR_TYPE_FCW20  0
#define SENSOR_TYPE_IBSM   1
#define SENSOR_TYPE_DTR    2
#define SENSOR_TYPE_BSM    3
#define SENSOR_TYPE_RCM    4
#define SENSOR_TYPE_FCW    5

extern "C"
{
 BET_API unsigned long BET_MIDDECL BET_LoadConfigFile(char *pCFGFile);
 BET_API unsigned long BET_MIDDECL BET_Testbumper(int nPlatformID,int nBumperType, long readOnlyTests, int *pnNumErrorCodes, int *pnErrorCode);
 BET_API unsigned long BET_MIDDECL BET_SetBumperType(int nPlatformCode, char* sChassisCode, long bAMG_BM, long bAMG_SA, int nColorCode); 
 BET_API unsigned long BET_MIDDECL BET_Retracing(int *nPlatformCode, char* sChassisCode, long *bAMG_BM, long *bAMG_SA, int *nColorCode, unsigned long *nDTROffsetSensor2,unsigned long *nDTROffsetSensor3); 
 BET_API unsigned long BET_MIDDECL BET_ErrorCodeToText(int nErrorCode,char *errorTextBuffer,int bufferSize);
 BET_API unsigned long BET_MIDDECL BET_GetSensorInfo(int nPlatformID ,int address,unsigned long *pProdNum,unsigned long *pYear,unsigned long *pWeek,unsigned long *pDay,unsigned long *pSerialNum);
 BET_API unsigned long BET_MIDDECL BET_GetSensorVer(int nPlatformID, int address,unsigned long *pmajVer,unsigned long *pminVer);
 BET_API unsigned long BET_MIDDECL BET_GetSensorVerExt(int nPlatformID, int address,unsigned long *pmajVer,unsigned long *pminVer,unsigned long *psubVer);
 BET_API unsigned long BET_MIDDECL BET_GetNumSensors();
 BET_API unsigned long BET_MIDDECL BET_FastPing(int nPlatformID, unsigned long *numSensorsDetected);
 BET_API unsigned long BET_MIDDECL BET_GetZBANumber(int nPlatformID, int address, char* ZBANumber);
}
#endif //MXBET_H
