/*****************************************************************************

  COMPANY:                Tyco Electronics - M/A-COM - Wireless Automotive
  
  CONFIGURATION ITEM:     tbet

  CPU:                    -

  COMPONENT:              -

  MODULE:                 -

  FILENAME:               tbet.h

  DESCRIPTION:            -

  ORIGINATOR:             Daniel Havlik

  DATE OF CREATION:       March 2, 2006

  CHANGES
  ---------------

  ARCHIVENAME:           $Source: /var/cvs/linuxtools/tbet/src/tbet.h,v $

  VERSIONNUMBER:         $Revision: 1.9 $

  EDITOR NAME:           $Author: dhavlik $

  DATE OF LAST EDIT:     $Date: 2010/10/22 13:31:08 $

  CHANGE:                $Log: tbet.h,v $
  CHANGE:                Revision 1.9  2010/10/22 13:31:08  dhavlik
  CHANGE:                Added call to return the sensor version.
  CHANGE:
  CHANGE:                Revision 1.8  2010/10/22 12:58:36  dhavlik
  CHANGE:                Added diagnostic option to disable clutter test recording. Also added progress notification.
  CHANGE:
  CHANGE:                Revision 1.7  2010/07/30 11:45:06  dhavlik
  CHANGE:                Added Create Config File Template function prototype.
  CHANGE:
  CHANGE:                Revision 1.6  2009/06/26 17:30:54  dhavlik
  CHANGE:                Added new error code for unsealed cfg.
  CHANGE:
  CHANGE:                Revision 1.5  2008/08/30 11:04:15  dhavlik
  CHANGE:                Slightly modified code to make it also work for Linux.
  CHANGE:
  CHANGE:                Revision 1.4  2008/04/15 11:31:14  dhavlik
  CHANGE:                Updated for Visual Basic and fixed a bug while comparing the sensor version.
  CHANGE:
  CHANGE:                Revision 1.3  2008/04/03 12:57:30  dhavlik
  CHANGE:                Modified the code to use configuration file, improved the test and added SIC11 support.
  CHANGE:
  CHANGE:                Revision 1.2  2006/03/03 14:44:20  dhavlik
  CHANGE:                Modified files to make it compatible with VC++. Added support for 216 platform and removed magic numbers.
  CHANGE:
  CHANGE:                Revision 1.1.1.1  2006/03/02 15:54:47  dhavlik
  CHANGE:                Initial checkin.
  CHANGE:


The information contained in this document or item is the property of M/A-COM,
Inc., A Tyco International Company, Tyco International Inc., and/or its 
subsidiaries hereinafter collectively ("M/A-COM"), and shall be kept in strict
confidence. Except with the written permission of M/A-COM, such information or
item shall not be published, or disclosed to others, or used for manufacture 
or sale or for any other purpose, and this document or item shall not be
reproduced, in whole or in part. If permission is granted for reproduction,
this legend shall be included in any such reproduction. This document or item
shall be returned to M/A-COM upon request, or completion of the use for which
it was made available to recipient, or termination of relationship with 
recipient, whichever first occurs. Any recipient so agrees by acceptance of 
this document or item. (Rev. 6/98) Copyright 2005, M/A-COM, Inc., A Tyco 
International Company, Unpublished. All rights under the copyright laws of 
the United States and other laws.
*****************************************************************************/

#if !defined TBET_H
#define TBET_H

// The following ifdef block is the standard way of creating macros which make exporting
// from a DLL simpler. All files within this DLL are compiled with the TBET_EXPORTS
// symbol defined on the command line. this symbol should not be defined on any project
// that uses this DLL. This way any other project whose source files include this file see
// TBET_API functions as being imported from a DLL, whereas this DLL sees symbols
// defined with this macro as being exported.
#ifdef WIN32
  #ifdef TBET_EXPORTS
    #define TBET_API __declspec(dllexport)
  #else
    #define TBET_API __declspec(dllimport)
  #endif
    
 #define TBET_MIDDECL   __stdcall   
#else
  #define TBET_API
  #define TBET_MIDDECL  
#endif

#include <string>

using namespace std;

#define SENSOR_1            (1)
#define SENSOR_2            (2)
#define SENSOR_3            (3)
#define SENSOR_4            (4)

#define FRONT_BUMPER  1
#define REAR_BUMPER   2
#define MAX_BUMPER_SENSORS  (4)

#define SEN_ERR_MASK 0x00FFFFFF
#define MAKE_ERRORCODE(m,e) (((m)<<24)|(e))
#define GET_ERROR(e) (SEN_ERR_MASK&(e))
#define GET_SENSMASK(e) ((e) >> 24)

#define ERR_NO_ERROR                  0  //Success
#define ERR_CAN_HARDWARE              1  //CAN-Hardware not valid
#define ERR_PLATFORM_NOT_VALID        2  //Platform ID not valid
#define ERR_BUMPER_NOT_VALID          3  //Bumpertype not valid
#define ERR_INVALID_SENSOR_PLACEMENT  4  //placement of the sensors differs from configuration
#define ERR_SENSOR_NO_ANSWER          5  //Sensor did not respond to ping
#define ERR_SENSOR_BAD_VERSION        6  //Sensor version differs from configuration
#define ERR_SENSOR_NOT_PROG           7  //Sensor has not been programmed
#define ERR_EEPROM_READ               8  
#define ERR_NO_SENSOR_DETECTED        9  //no sensor detected on the bus

#define ERR_VOLTAGE_LOW       10
#define ERR_VOLTAGE_HIGH      11
#define ERR_ADDRESS_UNSTABLE  12
#define ERR_INTERNAL_NOISE    13
#define ERR_INIT              14
#define ERR_CALIBRATION       15
#define ERR_HARDWARE          16

#define ERR_CLUTTER           17
#define ERR_CFG_NOT_SEALED    18
#define ERR_SW1               19     //Software error 1
#define ERR_SW2               20     //Software error 2
#define ERR_SW3               21     //Software error 3
#define ERR_SW4               22     //Software error 4
#define TBET_MAX_ERRORS      (ERR_SW4)



extern "C"
{
 TBET_API long TBET_MIDDECL TBET_LoadConfigFile(char *pCFGFile);
 TBET_API long TBET_MIDDECL TBET_Testbumper(int nPlatformID, int nBumperType,long readOnlyTests, int *pnNumErrorCodes, int *pnErrorCode);
 TBET_API long TBET_MIDDECL TBET_ErrorCodeToText(int nErrorCode,char *errorTextBuffer,int bufferSize);
 TBET_API long TBET_MIDDECL TBET_GetSensorSN(int nPlatformID, int nBumperType,int nSensorAddress);
 TBET_API long TBET_MIDDECL TBET_GetSensorMASR(int nPlatformID, int nBumperType,int nSensorAddress);
 TBET_API long TBET_MIDDECL TBET_GetNumSensors(int nPlatformID, int nBumperType);
 TBET_API long TBET_MIDDECL TBET_GetSensorVer(int nPlatformID, int nBumperType,int nSensorAddress,int *pmajVer,int *pminVer);
}
#endif //TBET_H
