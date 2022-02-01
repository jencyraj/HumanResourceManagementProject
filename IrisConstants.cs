using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace HRM.DAL
{
    public enum UserRole
    {
        None = 0,
        Administrator = 1,
        Operator = 2,
        Monitor = 4
    };

    public enum eRelayOperation
    {
        TimeOutFromWebConfig = -2,
        InfiniteTimeOut = -1,
        TurnOffImmediately = 0,
        TimeOutFromAppConfig = 1
    };

    public enum eRelayPorts
    {
        ICAM_GPIO_PORT_NC1 = 1,
        ICAM_GPIO_PORT_NC2
    };

    struct ProcessResults
    {
        int iProcessID;
        string strProcessResult;

        public ProcessResults(int a, string strVal)
        {
            iProcessID = a;
            strProcessResult = strVal;
        }

        public string GetProcessResultValue()
        {
            return strProcessResult;
        }

        public int GetProcessID()
        {
            return iProcessID;
        }
    } ;

    public class Constants
    {
        public const string TITLE = "iCAM Manager SDK";

        //DB File not Found
        public const int DB_FILE_NOT_FOUND = -2;
        public const int DB_TRUE = 1;
        public const int DB_FALSE = 0;
        public const int IRISCODE_LENGTH = 512;
        public const int IRISIMAGE_LENGTH = 307200;
        public const int MAX_FILES = 2;

        //Card Type           
        public const int CARDID_LENGTH = 70;
        public const int CARDNUMBER_LENGTH = 70;
        public const int JOBCODE_LENGTH = 7;

        //User Info
        public const int USERID_LENGTH = 42;
        public const int FIRSTNAME_LENGTH = 66;
        public const int LASTNAME_LENGTH = 66;
        public const int USER_NOT_ENROLLED = -1;
        public const int TIMEGROUP_ID_LIST_LENGTH = 10;
        public const int DEFAULT_TIMEGROUP_ID = 1;
        public const int PIN_LENGTH = 1000;
        public const int SECURITYID_LENGTH = 16;

        // Face Image (Live) Type
        public const int IS_FI_480X640 = 3;
        public const int IS_FI_240X320 = 2;
        public const int IS_FI_120X160 = 1;

        // Face Image (Captured) Type
        public const int IS_FI_CAPTURED_NONE = 1;
        public const int IS_FI_CAPTURED_ISO_480X640 = 2;
        public const int IS_FI_CAPTURED_RAW_1200X1600 = 3;
        public const int IS_FI_CAPTURED_RAW_1920X2560 = 4;
        public const int IS_FI_CAPTURED_JPEG_1200X1600 = 5;
        public const int IS_FI_CAPTURED_JPEG_1944X2592 = 6;

        //
        public const string DEFAULT_TIME = "00:00-00:00";
        public const int DEFAULT_INDEX = 0;
        public const int ERR_NONE = 0;
        public const int IRISCODE_TYPE = 3;
        public const int IRISCODE_VERSION = 1;

        //Transaction Log
        public const int OCCURDATE_LENGTH = 40;
        public const int MESSAGE_LENGTH = 102;
        public const int MAXBLOCK_SIZE = 1024;
        public const int DATE_TIME_ERROR = -1;

        //Face Image Dimensions
        public const int MAX_FACE_IMAGE_HEIGHT = 640;
        public const int MAX_FACE_IMAGE_WIDTH = 480;

        //TimeGroupInfo
        public const int NAME_SIZE = 66;
        public const int DESCRIPTION_SIZE = 102;
        public const int DAY_NUM = 8;
        public const int TIMELIST_SIZE = 40;
        public const int SCHEDULE_TIMELIST_SIZE = 50;

        //HolidayInfo
        public const int HOLIDAY_NAME_SIZE = 66;
        public const int HOLIDAY_DESCRIPTION_SIZE = 102;
        public const int HOLIDAY_DATE_SIZE = 40;

        //System Log Kind
        public const int SYSLOG_SDK_CLIENT_CONNECTED = 102;
        public const int SYSLOG_SDK_CLIENT_DISCONNECTED = 103;

        //Transaction Log Kind
        public const int PROCESS_RESULT_COUNT = 35;
        public const int TRANSACTIONLOG_ACCEPTED = 0;
        public const int TRANSACTIONLOG_DENIED_OR_NOT_VERIFIED = 1;
        public const int TRANSACTIONLOG_DENIED_IRIS_NOT_TRIED = 47;
        public const int TRANSACTIONLOG_DENIED_IRIS_NOT_MATCHED = 48;
        public const int TRANSACTIONLOG_DENIED_USR_ID_NOT_ENROLLED = 49;
        public const int TRANSACTIONLOG_ACCEPTED_NORMAL = 50;
        public const int TRANSACTIONLOG_ACCEPTED_INCORRECT_TIME = 51;
        public const int TRANSACTIONLOG_ACCEPTED_WARNING_EYE = 52;
        public const int TRANSACTIONLOG_ACCEPTED_SUPER_USER = 53;
        public const int TRANSACTIONLOG_DENIED_NO_ACCESS_FOR_DOOR = 54;
        public const int TRANSACTIONLOG_DENIED_NO_ACCESS_FOR_TIME = 55;
        public const int TRANSACTIONLOG_DENIED_EXPIRED = 56;
        public const int TRANSACTIONLOG_DENIED_IRIS_NOT_ENROLLED = 57;
        public const int TRANSACTIONLOG_DENIED_FAKE_EYE = 58;
        public const int TRANSACTIONLOG_DENIED_OVER_TIME = 59;
        public const int TRANSACTIONLOG_IRIS_NOT_VERIFIED = 60;
        public const int TRANSACTIONLOG_IDENTIFIED = 63;
        public const int TRANSACTIONLOG_NOT_IDENTIFIED_FAKE_EYE = 64;
        public const int TRANSACTIONLOG_NOT_IDENTIFIED_NOT_ENROLLED = 65;
        public const int TRANSACTIONLOG_VERIFIED = 66;
        public const int TRANSACTIONLOG_NOT_VERIFIED_FAKE_EYE = 67;
        public const int TRANSACTIONLOG_NOT_VERIFIED_NOT_ENROLLED = 68;
        public const int TRANSACTIONLOG_NOT_VERIFIED_PASSED_EYE_TEST = 69;
        public const int TRANSACTIONLOG_NOT_VERIFIED_FAIL_TO_FIND_CARD_ID = 70;
        public const int TRANSACTIONLOG_NOT_VERIFIED_VERIFICATION_TIME_OUT = 71;
        public const int TRANSACTIONLOG_NOT_VERIFIED_VERIFICATION_FAILED_NOT_PASSED_LIVE_TEST = 72;
        public const int TRANSACTIONLOG_NOT_VERIFIED_VERIFICATION_FAILED = 73;
        public const int TRANSACTIONLOG_NOT_VERIFIED_NOT_PASSED_LIVE_TEST = 74;
        public const int TRANSACTIONLOG_NOT_VERIFIED_NO_AUTHORITY_IN_VERIFICATION = 75;
        public const int TRANSACTIONLOG_NOT_VERIFIED_TIME_OUT_IN_VERIFICATION = 76;
        public const int TRANSACTIONLOG_DENIED_WRONG_FACILITY_CODE = 77;
        public const int TRANSACTIONLOG_DENIED_PARITY_ERROR = 78;
        public const int TRANSACTIONLOG_DENIED_WRONG_FACILITY_CODE_PARITY_ERROR = 79;
        public const int TRANSACTIONLOG_AUTHORIZED_BY_ACCESS_PANEL = 80;
        public const int TRANSACTIONLOG_UNAUTHORIZED_BY_ACCESS_PANEL = 81;
        public const int TRANSACTIONLOG_DENIED_INVALID_CARD_ID_IRIS_MATCHED = 82;
        public const int TRANSACTIONLOG_DENIED_CARDID_NOT_FOUND = 83;
        public const int TRANSACTIONLOG_UNKNOWN = 90;

        //Match Iris
        public const int FIRST_MATCH = 1;
        public const int BEST_MATCH = 2;

        //Iris Folder's
        public const string FOLDER_IRIS_CODE = "IrisCode";
        public const string FOLDER_IRIS_IMAGE = "IrisImage";
        public const string FOLDER_FACE_IMAGE = "FaceImage";
        public const string FILE_FACE_JPEG = "_F.jpeg";
        public const string FILE_FACE_BMP = "_F.bmp";
        public const string FILE_FACE_RAW = "_F.raw";
        public const string FILE_LEFT_EYE = "_LE.dat";
        public const string FILE_RIGHT_EYE = "_RE.dat";
        public const string BMP_LEFT_EYE = "_LE.bmp";
        public const string BMP_RIGHT_EYE = "_RE.bmp";
        public const string FILE_DELIMETER = "\\";
        public const string BMP_FORMAT = ".bmp";
        public const string DAT_FORMAT = ".dat";
        public const string RAW_FORMAT = ".raw";
        public const string JPG_FORMAT = ".jpeg";
        public const string ADD_USER = "Add";
        public const string LEFT_EYE = "_LE";
        public const string RIGHT_EYE = "_RE";
        public const string ERRORS = "Error";
        public const string THUMBS = "Thumbs.db";
        public const int ERROR = -1;
        public const string DEFAULT_VALUE = "0";
        public const int FOLDER_DOES_NOT_EXIST = -1;
        public const int IRIS_IMAGE_WIDTH_640 = 640;
        public const int IRIS_IMAGE_HEIGHT_480 = 480;
        public const int IRIS_CODE_DOES_NOT_EXIST = 57;

        //Fake Eye Level
        public const int DEFAULT_FAKE_EYE_LEVEL = 0;
        public const int MAX_FAKE_EYE_LEVEL = 1;

        //Iris Capture TimeOut
        public const int CAPTURE_TIME_OUT = 30;

        //Iris TRIAL Quality
        public const int IRIS_QUALITY_FIRST_TRIAL = 65;
        public const int IRIS_QUALITY_SECOND_TRIAL = 60;

        //Tabs
        public const string USER = "User";
        public const string FACE = "Face Image";
        public const string CARD = "Card";
        public const string IRIS = "Iris";
        public const string TIME_GROUP = "Time Group";
        public const string TRANSACTION_LOG = "Transaction Log";
        public const string SYSTEM_LOG = "System Log";
        public const string OPERATION_LOG = "Operation Log";
        public const string TRANSACTION = "Transaction";
        public const string SYSTEM = "System";
        public const string OPERATION = "Operation";
        public const string HOLIDAY = "Holiday";
        public const string LOG_NOTIFICATIONS = "Log Notifications";
        public const string GENERAL = "Device";
        public const string TAB_IRIS = "tabIrisInfo";
        public const string TAB_CARD = "tabCardInfo";
        public const string MODE_SCHEDULE = "Mode Schedules";
        public const string EXTERNAL_INTERFACE = "External Interface";

        //Camera Status
        public const string CAMERA_CONNECTED = "Connected";
        public const string CAMERA_DISCONNECTED = "Disconnected";
        public const string CAMERA_FAIL_TO_CONNECT = "Fail to Connect";
        public const string SERVER_IS_DISCONNECTED = " Server is Disconnected";
        public const string DEVICE_DETAILS = "DeviceDetails.txt";
        public const string UPLOAD_LOG = "UploadLog.txt";
        public const string SI_NO = "SI No";
        public const string CAMERA_STATUS = "Camera Status";
        public const string CONNECTION_TYPE = "Connection Type";
        public const string USER_ROLE_ID = "UserRoleID";
        public const string OPERATIONAL_MODE = "Operational Mode";
        public const string DELIMETER = ",";
        public const string TRANSACTION_LOG_DELIMETER = "(";

        //Log Options
        public const string GET_SYSTEM_LOGS = "Get System Logs";
        public const string DELETE_SYSTEM_LOGS = "Delete System Logs";
        public const string GET_OPERATION_LOGS = "Get Operation Logs";
        public const string DELETE_OPERATION_LOGS = "Delete Operation Logs";
        public const string GET_TRANSACTION_LOGS = "Get Transaction Logs";
        public const string DELETE_TRANSACTION_LOGS = "Delete Transaction Logs";
        public const string GET_AND_DELETE_ALL_LOGS = "Get and Delete All Logs";

        //Connection Type
        public const string READ_ONLY = "Read Only";
        public const string READ_WRITE = "Read and Write";
        public const string USERS_READ_WRITE_LOG_RO = "Users Read and Write Logs Read Only";

        //User Roles
        public const string ADMINISTRATOR = "Administrator";
        public const string OPERATOR = "Operator";
        public const string EAC_USER_READ_ONLY = "EACUser (Read Only)";
        public const string MONITOR = "Monitor";
        public const string SELECT_DEVICE = "--Select Device--";

        //Transaction Record Status
        public const string SENT_TO_SERVER = "Sent to Server";
        public const string NOT_SENT_TO_SERVER = "Not Sent to Server";

        //Start Date & EndDate , Visitor
        public const int DATE_SIZE = 40;
        public const int TIME_SIZE = 15;

        //Camera's Supported
        public const int MAX_DEVICE_ALLOWED = 100;

        //Transaction Log Filter's
        public const string USER_ID = "User ID";
        public const string CARD_ID = "Card ID";
        public const string FUNCTION_KEY = "Function Key";
        public const string PROCESS_RESULT = "Process Result";
        public const string TRANSACTION_RECORD_STATUS = "Transaction Record Status";
        public const string DATE_TIME_RANGE = "Date && Time Range";
        public const string JOB_CODE = "Job Code";
        public const string SET_SERVERFLAG = "Server Flag";

        //Operational Mode
        public const string EAC = "EAC Mode";
        public const string NON_EAC = "Non-EAC Mode";

        //Card Detection and Stop
        public const int CARD_TIMEOUT = 10000; //in Milliseconds
        public const int CARD_WIEGAND_TIME_OUT = 30000; //in Milliseconds

        //Set iCAM Mode
        public const string RECOGNITION = "Recognition Mode";
        public const string ENROLL = "Enrollment Mode";
        public const int iCAM_MODE_MIN_VALUE = 5; //in Seconds
        public const int iCAM_MODE_MAX_VALUE = 30; //in Seconds
        public const int CARD_TIMEOUT_MIN_VALUE = 1; //in Seconds
        public const int CARD_TIMEOUT_MAX_VALUE = 10; //in Seconds
    }

    public class ErrorConstants
    {
        //ICAM SDK Error Codes
        public const int ICAMSDK_ERROR_NONE = unchecked((int)0x00000000);
        public const int ICAMSDK_ERROR_IN_OPEN = unchecked((int)0x80002101);
        public const int ICAMSDK_ERROR_IN_CONNECT = unchecked((int)0x80002102);
        public const int ICAMSDK_ERROR_IN_REQ_DATA = unchecked((int)0x80002103);
        public const int ICAMSDK_ERROR_IN_PARAMETER = unchecked((int)0x80002104);
        public const int ICAMSDK_ERROR_IN_USER_ID_LENGTH = unchecked((int)0x80002105);
        public const int ICAMSDK_ERROR_IN_COMMAND = unchecked((int)0x80002106);
        public const int ICAMSDK_ERROR_IN_RESP_DATA = unchecked((int)0x80002107);
        public const int ICAMSDK_ERROR_IN_RESP_DATA_SIZE = unchecked((int)0x80002108);
        public const int ICAMSDK_ERROR_IN_INTERFACE = unchecked((int)0x80002109);
        public const int ICAMSDK_ERROR_IN_PARAMETER_SIZE = unchecked((int)0x80002110);
        public const int ICAMSDK_ERROR_IN_ENCRYPTION = unchecked((int)0x80002111);
        public const int ICAMSDK_SDK_ERROR_IN_RESPONSE_PACKET = unchecked((int)0x80002112);
        public const int ICAMSDK_ERROR_TRANSACTION_LOG_RECORDS_SIZE = unchecked((int)0x80002113);
        public const int ICAMSDK_ERROR_IN_SECURITY_ID_RESPONSE = unchecked((int)0x80002114);
        public const int ICAMSDK_ERROR_IN_RESPONSE_FRAME_TYPE = unchecked((int)0x80002115);
        public const int ICAMSDK_ERROR_SERVER_DISCONNECTED = unchecked((int)0x80002116);
        public const int ICAMSDK_ERROR_IN_SECURITYID_REQ_DATA = unchecked((int)0x80002117);
        public const int ICAMSDK_ERROR_IN_SECURITYID_RESP_DATA = unchecked((int)0x80002118);
        public const int ICAMSDK_ERROR_IN_LOGIN_REQ_DATA = unchecked((int)0x80002119);
        public const int ICAMSDK_ERROR_IN_LOGIN_RESP_DATA = unchecked((int)0x80002120);
        public const int ICAMSDK_ERROR_IN_LOGIN_RESP_DATA_SIZE = unchecked((int)0x80002121);
        public const int ICAMSDK_ERROR_IN_DATE_SIZE = unchecked((int)0x80002122);
        public const int ICAMSDK_ERROR_IN_IRISINFO_EYETYPE = unchecked((int)0x80002123);
        public const int ICAMSDK_ERROR_RECOGVERSION_IS_GREATER = unchecked((int)0x80002124);
        public const int ICAMSDK_ERROR_SDKVERSION_IS_GREATER = unchecked((int)0x80002125);
        public const int ICAMSDK_ERROR_IN_RIGHTIRIS_EYETYPE = unchecked((int)0x80002126);
        public const int ICAMSDK_ERROR_IN_LEFTIRIS_EYETYPE = unchecked((int)0x80002127);
        public const int ICAMSDK_ERROR_IN_EYETYPE = unchecked((int)0x80002128);
        public const int ICAMSDK_ERROR_IN_USER_ID_CANOT_BE_EMPTY = unchecked((int)0x80002129);
        public const int ICAMSDK_ERROR_SECURITYID_LENGTH = unchecked((int)0x80002130);
        public const int ICAMSDK_ERROR_GETFIRST_NOT_CALLED = unchecked((int)0x80002131);
        public const int ICAMSDK_ERROR_IN_DECRYPTION = unchecked((int)0x80002132);
        public const int ICAMSDK_ERROR_USER_RECORDS_SIZE = unchecked((int)0x80002133);
        public const int ICAMSDK_ERROR_TIMEGROUP_RECORDS_SIZE = unchecked((int)0x80002134);
        public const int ICAMSDK_ERROR_HOLIDAY_RECORDS_SIZE = unchecked((int)0x80002135);
        public const int ICAMSDK_ERROR_IN_ADD_HOLIDAY_RESP_SIZE = unchecked((int)0x80002136);
        public const int ICAMSDK_ERROR_IN_UPDATE_HOLIDAY_RESP_SIZE = unchecked((int)0x80002137);
        public const int ICAMSDK_ERROR_SYSTEMLOG_RECORDS_SIZE = unchecked((int)0x80002138);
        public const int ICAMSDK_ERROR_IN_FRAME_TYPE = unchecked((int)0x80002139);
        public const int ICAMSDK_ERROR_IN_DATA_SIZE = unchecked((int)0x80002140);
        public const int ICAMSDK_ERROR_OPERATIONLOG_RECORDS_SIZE = unchecked((int)0x80002141);
        public const int ICAMSDK_ERROR_INVALID_DATE_TIME = unchecked((int)0x80002142);
        public const int ICAMSDK_ERROR_INVALID_PIN_NUMBER = unchecked((int)0x80002143);
        public const int ICAMSDK_ERROR_END_DATE_IS_LESSTHAN_START_DATE = unchecked((int)0x80002144);
        public const int ICAMSDK_ERROR_IN_IRISIMAGE_DATA = unchecked((int)0x80002145);
        public const int ICAMSDK_ERROR_IRISCODE_DATA_SIZE = unchecked((int)0x80002146);
        public const int ICAMSDK_ERROR_IRISCODE_MATCHMODE = unchecked((int)0x80002147);
        public const int ICAMSDK_ERROR_IN_FAKEEYE_LEVEL = unchecked((int)0x80002148);
        public const int ICAMSDK_ERROR_IN_CAPTURE_TIMEOUT = unchecked((int)0x80002149);
        public const int ICAMSDK_ERROR_TX_FILTER_DATA = unchecked((int)0x80002150);
        public const int ICAMSDK_ERROR_IN_IPADDRESS_FORMAT = unchecked((int)0x80002151);
        public const int ICAMSDK_ERROR_IN_CONNECTION_OPTIONS = unchecked((int)0x80002152);
        public const int ICAMSDK_ERROR_QUALITYTYPE_IRIS_IMAGE_NOT_SUPPORTED = unchecked((int)0x80002153);
        public const int ICAMSDK_ERROR_IN_IRIS_CODE_QUALITY = unchecked((int)0x80002154);
        public const int ICAMSDK_ERROR_IN_LEFT_IRIS_CODE_QUALITY = unchecked((int)0x80002155);
        public const int ICAMSDK_ERROR_IN_RIGHT_IRIS_CODE_QUALITY = unchecked((int)0x80002156);
        public const int ICAMSDK_ERROR_IN_CARD_TIMEOUT = unchecked((int)0x80002157);
        public const int ICAMSDK_ERROR_IN_TIMEOUT = unchecked((int)0x80002158);
        public const int ICAMSDK_ERROR_INVALID_USER_TYPE = unchecked((int)0x80002159);
        public const int ICAMSDK_ERROR_INVALID_DATE_TIME_FOR_USER_TYPE_VISITOR = unchecked((int)0x80002160);
        public const int ICAMSDK_ERROR_START_DATE_SHOULD_BE_ENTERED = unchecked((int)0x80002161);
        public const int ICAMSDK_ERROR_IN_NEW_USER_ID_LENGTH = unchecked((int)0x80002162);
        public const int ICAMSDK_ERROR_USER_ID_CANNOT_BE_NULL = unchecked((int)0x80002163);
        public const int ICAMSDK_ERROR_IN_CARDKIND_PARAMETER = unchecked((int)0x80002164);
        public const int ICAMSDK_ERROR_CARD_ID_IS_EMPTY = unchecked((int)0x80002165);
        public const int ICAMSDK_ERROR_CARD_NUMBER_IS_EMPTY = unchecked((int)0x80002166);
        public const int ICAMSDK_ERROR_IN_CARD_ID_LENGTH = unchecked((int)0x80002167);
        public const int ICAMSDK_ERROR_IN_CARD_NUMBER_LENGTH = unchecked((int)0x80002168);
        public const int ICAMSDK_ERROR_INVALID_END_DATE_TIME_FOR_USER_TYPE_VISITOR = unchecked((int)0x80002169);
        public const int ICAMSDK_ERROR_INVALID_START_DATE_TIME_FOR_USER_TYPE_VISITOR = unchecked((int)0x80002170);
        public const int ICAMSDK_ERROR_HOLIDAY_NAME_IS_MANDATORY = unchecked((int)0x80002171);
        public const int ICAMSDK_ERROR_TIMEGROUP_NAME_IS_MANDATORY = unchecked((int)0x80002172);
        public const int ICAMSDK_ERROR_INVALID_TIMELIST = unchecked((int)0x80002173);
        public const int ICAMSDK_ERROR_TIMEINFO_NOT_SET = unchecked((int)0x80002174);
        public const int ICAMSDK_ERROR_STARTTIME_SHOULD_BE_LESSTHAN_ENDTIME = unchecked((int)0x80002175);
        public const int ICAMSDK_ERROR_TIMECOUNT_IS_GREATERTHAN_TIMELIST = unchecked((int)0x80002176);
        public const int ICAMSDK_ERROR_DUPLICATE_TIME_PERIODS_EXIST = unchecked((int)0x80002177);
        public const int ICAMSDK_ERROR_TIMEINFO_HOURS_SHOULD_NOT_BE_GREATER_THAN_24 = unchecked((int)0x80002178);
        public const int ICAMSDK_ERROR_TIMEINFO_MINUTES_SHOULD_NOT_BE_GREATER_THAN_59 = unchecked((int)0x80002179);
        public const int ICAMSDK_ERROR_DEVICENAME_LENGTH = unchecked((int)0x80002180);
        public const int ICAMSDK_ERROR_SERVER_ALREADY_CONNECTED = unchecked((int)0x80002181);
        public const int ICAMSDK_ERROR_OLD_AND_NEW_LOGINDETAILS_CANOT_BE_SAME = unchecked((int)0x80002182);
        public const int ICAMSDK_ERROR_USERNAME_AND_PASSWORD_CANOT_BE_SAME = unchecked((int)0x80002183);
        public const int ICAMSDK_ERROR_IN_USERNAME_LENGTH = unchecked((int)0x80002184);
        public const int ICAMSDK_ERROR_IN_PASSWORD_LENGTH = unchecked((int)0x80002185);
        public const int ICAMSDK_ERROR_ANOTHER_REQUEST_IS_IN_PROGRESS = unchecked((int)0x80002186);
        public const int ICAMSDK_ERROR_NOT_CONNECTED_TO_ANY_DEVICE = unchecked((int)0x80002187);
        public const int ICAMSDK_ERROR_IN_MEMORYCREATION = unchecked((int)0x80002188);
        public const int ICAMSDK_ERROR_SOFTWARE_DATE_IS_EXPIRED = unchecked((int)0x80002189);
        public const int ICAMSDK_ERROR_UNKNOWN = unchecked((int)0x80002190);

        public const int ICAMSDK_ERROR_CONNECTION_LIMIT_REACHED = unchecked((int)0x80002191);
        public const int ICAMSDK_ERROR_USER_LIMIT_REACHED = unchecked((int)0x80002192);
        public const int ICAMSDK_ERROR_INVALID_GALLERY_SIZE = unchecked((int)0x80002193);
        public const int ICAMSDK_ERROR_IN_LICENSE_INITIALIZE = unchecked((int)0x80002250);

        public const int ICAMSDK_ERROR_IMAGETYPE_NOT_SUPPORTED = unchecked((int)0x80002252);
        public const int ICAMSDK_ERROR_IMAGEWIDTH_NOT_SUPPORTED = unchecked((int)0x80002253);
        public const int ICAMSDK_ERROR_IMAGEHEIGHT_NOT_SUPPORTED = unchecked((int)0x80002254);
        public const int ICAMSDK_ERROR_IN_FACEIMAGE_CONVERSION = unchecked((int)0x80002255);
        public const int ICAMSDK_ERROR_IN_FACEIMAGE_SIZE = unchecked((int)0x80002256);
        public const int ICAMSDK_ERROR_IN_SYS_CONFIG_DATA = unchecked((int)0x80002257);
        public const int ICAMSDK_ERROR_SCHEDULE_OVERLAP = unchecked((int)0x80002258);

        //IAClient DB Defs
        public const int IACDB_ERROR_NONE = 0x0000;
        public const int IACDB_ERROR_OPEN = 0x0001;
        public const int IACDB_ERROR_CLOSE = 0x0002;
        public const int IACDB_ERROR_CONNECTION = 0x0003;
        public const int IACDB_ERROR_SIZE = 0x0004;
        public const int IACDB_ERROR_PARAMETER = 0x0005;
        public const int IACDB_ERROR_NOT_UNIQUE = 0x0006;
        public const int IACDB_ERROR_NOT_OPEN = 0x0007;
        public const int IACDB_ERROR_NO_RECORD = 0x0008;
        public const int IACDB_ERROR_MEMORY = 0x0009;
        public const int IACDB_ERROR_MANDATORY = 0x0010;
        public const int IACDB_ERROR_QUERY = 0x0011;
        public const int IACDB_ERROR_CHECKSUM = 0x0012;
        public const int IACDB_ERROR_UNKNOWN = 0x0013;
        public const int IACDB_ERROR_DATE = 0x0014;
        public const int IACDB_ERROR_NOT_SELECT = 0x0015;
        public const int IACDB_ERROR_NOT_DESELECT = 0x0016;
        public const int IACDB_ERROR_CREATE_LIST = 0x0017;
        public const int IACDB_ERROR_RECORDSET = 0x0018;
        public const int IACDB_ADD_SKIPPED_MAXLIMIT = 0x0019;


        //Recog7000 SDK Error Codes
        public const int RECOG70003SDK_ERR_NONE = 0x00000000;
        public const int RECOG70003SDK_ERR_FRAME_SIZE = unchecked((int)0x11000001);
        public const int RECOG70003SDK_ERR_PARAMETER = unchecked((int)0x11000002);
        public const int RECOG70003SDK_ERR_ENCRYPT = unchecked((int)0x11000003);
        public const int RECOG70003SDK_ERR_DECRYPT = unchecked((int)0x11000004);
        public const int RECOG70003SDK_ERR_DB = unchecked((int)0x11000005);
        public const int RECOG70003SDK_ERR_NO_RECORD = unchecked((int)0x11000006);
        public const int RECOG70003SDK_ERR_NOT_UNIQUE = unchecked((int)0x11000007);
        public const int RECOG70003SDK_ERR_INCORRECT_SID = unchecked((int)0x11000008);
        public const int RECOG70003SDK_ERR_USRID_PWD = unchecked((int)0x11000009);
        public const int RECOG70003SDK_ERR_REQ_FRAME_SIZE_PARAMETER = unchecked((int)0x11000010);
        public const int RECOG70003SDK_ERR_BLOCK_SIZE_PARAMETER = unchecked((int)0x11000011);
        public const int RECOG70003SDK_ERR_GETFIRST_TOBE_CALLED = unchecked((int)0x11000012);

        ////////////////// added for SDK by Praveena 08-02-2012
        public const int RECOG70003SDK_ERROR_USERID_NOT_EXIST = unchecked((int)0x11000013);
        public const int RECOG70003SDK_ERROR_CARDID_EXIST = unchecked((int)0x11000014);
        public const int RECOG70003SDK_ERROR_CARD_DETAILS_EXIST = unchecked((int)0x11000015);
        public const int RECOG70003SDK_ERROR_DUPLICATE_CARD_RECORDS = unchecked((int)0x11000016);
        public const int RECOG70003SDK_ERROR_CARD_DETAILS_NOT_EXIST = unchecked((int)0x11000017);
        public const int RECOG70003SDK_ERR_IRIS_EXIST = unchecked((int)0x11000018);
        public const int RECOG70003SDK_ERR_TGID_IS_INVALID = unchecked((int)0x11000019);
        public const int RECOG70003SDK_ERR_TG_ALREADY_ASSIGNED = unchecked((int)0x11000020);
        public const int RECOG70003SDK_ERR_TG_NOT_ASSIGNED = unchecked((int)0x11000021);
        public const int RECOG70003SDK_ERR_MAX_TG_ASSIGNED = unchecked((int)0x11000022);
        public const int RECOG70003SDK_ERR_FRAME_TYPE = unchecked((int)0x11000023);
        public const int RECOG70003SDK_ERR_SECURITYID_SIZE = unchecked((int)0x11000024);
        public const int RECOG70003SDK_ERR_PIN_IS_NOT_UNIQUE = unchecked((int)0x11000025);
        public const int RECOG70003SDK_ERR_USERID_IS_NOT_UNIQUE = unchecked((int)0x11000026);
        public const int RECOG70003SDK_ERR_TG_NAME_NOT_UNIQUE = unchecked((int)0x11000027);
        public const int RECOG70003SDK_ERR_HOLIDAY_NAME_NOT_UNIQUE = unchecked((int)0x11000028);
        public const int RECOG70003SDK_ERR_CAPTURE_FAILED = unchecked((int)0x11000029);
        public const int RECOG70003SDK_ERR_DUPLICATE_IRIS_FOUND = unchecked((int)0x11000030);
        public const int RECOG70003SDK_ERR_TEMPLATE_CREATION = unchecked((int)0x11000031);
        public const int RECOG70003SDK_ERR_TEMPLATE_ENCRYPTION = unchecked((int)0x11000032);
        public const int RECOG70003SDK_ERR_VERIFICATION_FAILED = unchecked((int)0x11000033);
        public const int RECOG70003SDK_ERR_ENROLLMENT_IN_PROGRESS = unchecked((int)0x11000034);
        public const int RECOG70003SDK_ERR_ENROLLMENT_NOT_IN_PROGRESS = unchecked((int)0x11000035);
        public const int RECOG70003SDK_ERR_ENROLLMENT_CANCELLED = unchecked((int)0x11000036);
        public const int RECOG70003SDK_ERR_CMD_NOT_SUPPORTED = unchecked((int)0x11000037);
        public const int RECOG70003SDK_ERR_CLIENT_ALREADY_CONNECTED = unchecked((int)0x11000038);
        public const int RECOG70003SDK_ERR_USER_LIMIT_REACHED = unchecked((int)0x11000039);
        public const int RECOG70003SDK_ERR_IRISCODE_LIMIT_REACHED = unchecked((int)0x11000040);
        public const int RECOG70003SDK_ERR_INVALID_FILTER_DATA = unchecked((int)0x11000041);
        public const int RECOG70003SDK_ERR_IN_MATCH_MODE = unchecked((int)0x11000042);
        public const int RECOG70003SDK_ERR_INVALID_FILTER_TYPE = unchecked((int)0x11000043);
        public const int RECOG70003SDK_ERR_INVALID_CONNECTION_TYPE = unchecked((int)0x11000044);
        public const int RECOG70003SDK_ERR_ACCEPTED_RO_CONNECTION = unchecked((int)0x11000045);
        public const int RECOG7000SDK_ERR_GET_SYS_INFO = unchecked((int)0x11000046);
        public const int RECOG7000SDK_ERR_POOR_IRIS_CODE_QUALITY = unchecked((int)0x11000047);
        public const int RECOG7000SDK_ERR_POOR_IRIS_IMAGE_QUALITY = unchecked((int)0x11000048);
        public const int RECOG7000SDK_ERR_MAX_TG_LIMIT_REACHED = unchecked((int)0x11000049);
        public const int RECOG7000SDK_ERR_MAX_HOLIDAY_LIMIT_REACHED = unchecked((int)0x11000050);
        public const int RECOG70003SDK_ERR_ALREADYIN_ENROLLMODE = unchecked((int)0x11000051);
        public const int RECOG70003SDK_ERR_ALREADYIN_RECOGNITION = unchecked((int)0x11000052);
        public const int RECOG70003SDK_ERR_DEVICE_IS_NOT_IN_ENROLLMODE = unchecked((int)0x11000053);
        public const int RECOG70003SDK_ERR_CARD_DETECTION_NOT_STARTED = unchecked((int)0x11000054);
        public const int RECOG70003SDK_ERR_CMD_NOT_VALID_FOR_ENROLL_MODE = unchecked((int)0x11000055);
        public const int RECOG70003SDK_ERR_CMD_NOT_VALID_FOR_RECOG_MODE = unchecked((int)0x11000056);
        public const int RECOG70003SDK_ERR_CARD_DETECTION_ALREADY_STARTED = unchecked((int)0x11000057);
        public const int RECOG70003SDK_ERR_MODE_IS_NOT_CONFIG_TO_SMARTCARD = unchecked((int)0x11000058);
        public const int RECOG70003SDK_ERR_CARD_DETECTION_IS_INPROGRESS = unchecked((int)0x11000059);
        public const int RECOG70003SDK_ERR_USER_LOGIN_DETAILS_INVALID = unchecked((int)0x11000060);
        public const int RECOG70003SDK_ERR_FAILED_TO_UPDATE_LOGINDETAILS = unchecked((int)0x11000061);
        public const int RECOG70003SDK_ERR_IRIS_FOUND_FOR_DIFF_USER = unchecked((int)0x11000062);
        public const int RECOG70003SDK_ERR_FIRSTIRISTEMPLATE_CREATION_NOT_ALLOWED = unchecked((int)0x11000064);
        public const int RECOG70003SDK_ERR_IRIS_EXIST_BUT_NOT_MATCHED = unchecked((int)0x11000065);

        //Face Image Error Codes
        public const int RECOG70003SDK_ERR_FACE_DETAILS_EXIST = unchecked((int)0x11000066);
        public const int RECOG70003SDK_ERR_FACE_DETAILS_NOT_EXIST = unchecked((int)0x11000067);
        public const int RECOG70003SDK_ERR_FACEIMAGE_SIZE = unchecked((int)0x11000068);
        public const int RECOG70003SDK_ERR_CANT_CREATE_FACEIMAGE = unchecked((int)0x11000069);
        public const int RECOG70003SDK_ERR_CANT_READ_FACEIMAGE = unchecked((int)0x11000070);
        public const int RECOG70003SDK_ERR_FACEIAMGE_DELETE_FAILED = unchecked((int)0x11000071);
        public const int RECOG70003SDK_ERR_FACEIMAGE_TYPE = unchecked((int)0x11000072);
        public const int RECOG70003SDK_ERR_FACECAPTURE_CANCELLED = unchecked((int)0x11000073);
        public const int RECOG70003SDK_ERR_FACECAPTURE_NOT_IN_PROGRESS = unchecked((int)0x11000074);
        public const int RECOG70003SDK_ERR_FACECAPTURE_IN_PROGRESS = unchecked((int)0x11000075);
        public const int RECOG70003SDK_ERR_FACECAPTURE_IMAGETYPE = unchecked((int)0x11000076);
        //Misc Error codes
        public const int RECOG70003SDK_ERR_UNKNOWN = unchecked((int)0x11000077);
        public const int RECOG70003SDK_ERR_ONDEVICEENROLL_INPROGRESS = unchecked((int)0x11000078);
        public const int RECOG7000SDK_ERR_GET_SYS_CONFIG = unchecked((int)0x11000079);
        public const int RECOG7000SDK_ERR_SET_SYS_CONFIG = unchecked((int)0x11000080);
        public const int RECOG7000SDK_ERR_HOLIDAY_EXISTS = unchecked((int)0x11000081);
        public const int RECOG7000SDK_ERR_MAX_SCHEDULE_LIMIT_REACHED = unchecked((int)0x11000082);
        public const int ICAMSDK_ERROR_RELAYPORT_NOTSUPPORTED = unchecked((int)0x80002259);
        public const int SET_RELAY_ERROR_MESSAGE = unchecked((int)0x80002260);
        //Security Error Codes
        public const int IASECURITY_ERROR_NONE = 0;
        public const int IASECURITY_ERROR_PARAMETER = -1000;
        public const int IASECURITY_ERROR_CREATE_PK = -1001;
        public const int IASECURITY_ERROR_DATA_SIZE = -1002;
        public const int IASECURITY_ERROR_CRYPTOPP = -1003;
        public const int IASECURITY_ERROR_STD = -1004;
        public const int IASECURITY_ERROR_VERIFY = -1005;
        public const int IASECURITY_ERROR_UNKNOWN = -9999;

        //IASocket Error Codes
        public const int IASOCKET_ERROR_NONE = unchecked((int)0x00000000);
        public const int IASOCKET_ERROR_OPEN = unchecked((int)0x80002001);
        public const int IASOCKET_ERROR_CLOSE = unchecked((int)0x80002002);
        public const int IASOCKET_ERROR_SEND = unchecked((int)0x80002003);
        public const int IASOCKET_ERROR_RECEIVE = unchecked((int)0x80002004);
        public const int IASOCKET_ERROR_RESPOND = unchecked((int)0x80002005);
        public const int IASOCKET_ERROR_MESSAGE = unchecked((int)0x80002006);
        public const int IASOCKET_ERROR_PARAMETER = unchecked((int)0x80002007);
        public const int IASOCKET_ERROR_DATA_SIZE = unchecked((int)0x80002008);
        public const int IASOCKET_ERROR_CANCEL = unchecked((int)0x80002009);
        public const int IASOCKET_ERROR_DISCONNECT_CLIENT = unchecked((int)0x80002024);
        public const int IASOCKET_ERROR_CONNECT_SERVER = unchecked((int)0x80002025);
        public const int IASOCKET_ERROR_DISCONNECT_SERVER = unchecked((int)0x80002026);
        public const int IASOCKET_ERROR_MEMORY = unchecked((int)0x80002027);
        public const int IASOCKET_ERROR_EVENT_TIME_OUT = unchecked((int)0x80002028);
        public const int IASOCKET_ERROR_UNKNOWN = unchecked((int)0x8000FFFF);

        public const int SDK_SC_DETECT_TIMEOUT = 0;
        public const int SDK_SC_DETECT_SUCCESS = 1;
        public const int SDK_SC_STAT_DISCONNECT = 2;
        public const int SDK_SC_DETECT_INVALID_CARD = 3;
        public const int ICAMSDK_ERROR_IN_SMARTCARD_DATA_READ = 4;

        public const int ICAMSDK_ERROR_IN_WIEGAND_DATA_READ = 2;

        public const int ERR_LFW_FLXERROR_CREATE = unchecked((int)0x79000001);
        public const int ERR_LFW_XMLFILE_READ = unchecked((int)0x79000002);
        public const int ERR_LFW_DATACONVERSION = unchecked((int)0x79000003);
        public const int ERR_LFW_STOREPATH = unchecked((int)0x79000004);
        public const int ERR_LFW_SAVETRIAL = unchecked((int)0x79000005);
        public const int ERR_LFW_WINDBACK = unchecked((int)0x79000006);
        public const int ERR_LFW_COPY_FILE = unchecked((int)0x79000007);
        public const int ERR_LFW_PARAMETER = unchecked((int)0x79000008);
        public const int ERR_LFW_METER = unchecked((int)0x79000009);
        public const int ERR_LFW_LICENSE_IS_INUSE = unchecked((int)0x79000010);
        public const int ERR_LFW_FILEOPEN = unchecked((int)0x79000011);
        public const int ERR_LFW_TRIAL_ALREADY_LOADED = unchecked((int)0x79000012);
        public const int ERR_LFW_TRIAL_EXPIRED = unchecked((int)0x79000013);
        public const int ERR_LFW_TRIAL_INVALID_ID = unchecked((int)0x79000014);
        public const int ERR_LFW_FEATURE_VERSION_NOT_FOUND = unchecked((int)0x79000015);
        public const int ERR_LFW_FEATURE_NOT_STARTED = unchecked((int)0x79000016);
        public const int ERR_LFW_DATE_INVALID = unchecked((int)0x79000017);
        public const int ERR_LFW_FEATURE_EXPIRED = unchecked((int)0x79000018);
        public const int ERR_LFW_LICENSE_NOT_FOUND = unchecked((int)0x79000019);
        public const int ERR_LFW_LICENSE_SOURCE_TYPE_INVALID = unchecked((int)0x79000020);
        public const int ERR_LFW_WINDBACK_DETECTED = unchecked((int)0x79000021);
        public const int ERR_LFW_WINDBACK_NOT_ENABLED = unchecked((int)0x79000022);
        public const int ERR_LFW_FEATURE_INSUFFICIENT_COUNT = unchecked((int)0x79000023);
        public const int ERR_LFW_HOSTID_INVALID = unchecked((int)0x79000024);
        public const int ERR_LFW_DATA_CORRUPTED = unchecked((int)0x79000025);
        public const int ERR_LFW_RESPONSE_INVALID = unchecked((int)0x79000026);
        public const int ERR_LFW_FEATURE_INVALID = unchecked((int)0x79000027);
        public const int ERR_LFW_TS_CORRUPTED = unchecked((int)0x79000028);
        public const int ERR_LFW_IDENTITY_INVALID = unchecked((int)0x79000029);
        public const int ERR_LFW_VENDORKEY_INVALID = unchecked((int)0x79000030);
        public const int ERR_LFW_CALLOUT_ERROR = unchecked((int)0x79000031);
        public const int ERR_LFW_UUID_INVALID = unchecked((int)0x79000032);
        public const int ERR_LFW_SIGNATURE_INVALID = unchecked((int)0x79000033);
        public const int ERR_LFW_PUBLISHER_DATA_NOT_SET = unchecked((int)0x79000034);
        public const int ERR_LFW_FEATURE_HOST_ID_MISMATCH = unchecked((int)0x79000035);
        public const int ERR_LFW_TS_HOST_ID_MISMATCH = unchecked((int)0x79000036);
        public const int ERR_LFW_FEATURE_NOT_FOUND = unchecked((int)0x79000037);
        public const int ERR_LFW_TS_ANCHOR_BREAK = unchecked((int)0x79000038);
        public const int ERR_LFW_TS_BINDING_BREAK = unchecked((int)0x79000039);
        public const int ERR_LFW_TS_DOES_NOT_EXIST = unchecked((int)0x79000040);
        public const int ERR_LFW_IDENTITY_UNSUPPORTED_VERSION = unchecked((int)0x79000041);
        public const int ERR_LFW_VENDOR_KEYS_EXPIRED = unchecked((int)0x79000042);
        public const int ERR_LFW_VENDOR_KEYS_NOT_ENABLED = unchecked((int)0x79000043);
        public const int ERR_LFW_ITEM_NOT_FOUND = unchecked((int)0x79000044);
        public const int ERR_LFW_ITEM_TYPE_MISMATCH = unchecked((int)0x79000045);
        public const int ERR_LFW_INDEX_OUT_OF_BOUND = unchecked((int)0x79000046);
        public const int ERR_LFW_FEATURE_SERVER_HOST_ID_MISMATCH = unchecked((int)0x79000047);
        public const int ERR_LFW_SHORT_CODE_LICENSE_EXPIRED = unchecked((int)0x79000048);
        public const int ERR_LFW_VERSION_STRING_INVALID = unchecked((int)0x79000049);
        public const int ERR_LFW_LICENSE_ERROR = unchecked((int)0x79000050);

    }

    class ErrorHandler
    {
        public static string ProcessError(long iErrorCode, string strIPAddress)
        {
            string strErrorDescription = string.Empty;

            if ((iErrorCode > ErrorConstants.ERR_LFW_FLXERROR_CREATE - 1) && (iErrorCode < ErrorConstants.ERR_LFW_LICENSE_ERROR + 1))
            {
                return GetLicenseError(iErrorCode);
            }

            switch (iErrorCode)
            {
                //iCAM7000-3 Error Codes.
                case ErrorConstants.ICAMSDK_ERROR_IN_OPEN:
                    strErrorDescription = "Error in Open";
                    break;
                case ErrorConstants.ICAMSDK_ERROR_IN_CONNECT:
                    strErrorDescription = "Error in Connecting to Camera " + strIPAddress;
                    break;
                case ErrorConstants.ICAMSDK_ERROR_IN_SECURITY_ID_RESPONSE:
                    strErrorDescription = "Error in Security ID Validation Response";
                    break;
                case ErrorConstants.ICAMSDK_ERROR_IN_COMMAND:
                    strErrorDescription = "Error in Command";
                    break;
                case ErrorConstants.ICAMSDK_ERROR_IN_ENCRYPTION:
                    strErrorDescription = "Error in Encryption";
                    break;
                case ErrorConstants.ICAMSDK_ERROR_IN_INTERFACE:
                    strErrorDescription = "Error in Interface";
                    break;
                case ErrorConstants.ICAMSDK_ERROR_IN_PARAMETER:
                    strErrorDescription = "Error in Parameter";
                    break;
                case ErrorConstants.ICAMSDK_ERROR_IN_PARAMETER_SIZE:
                    strErrorDescription = "Error in Parameter Size";
                    break;
                case ErrorConstants.ICAMSDK_ERROR_IN_REQ_DATA:
                    strErrorDescription = "Error in Request Data";
                    break;
                case ErrorConstants.ICAMSDK_ERROR_IN_RESP_DATA:
                    strErrorDescription = "Error in Response Data";
                    break;
                case ErrorConstants.ICAMSDK_ERROR_IN_RESP_DATA_SIZE:
                    strErrorDescription = "Error in Response Data Size";
                    break;
                case ErrorConstants.ICAMSDK_ERROR_IN_RESPONSE_FRAME_TYPE:
                    strErrorDescription = "Error in Response Frame Type";
                    break;
                case ErrorConstants.ICAMSDK_ERROR_IN_USER_ID_LENGTH:
                    strErrorDescription = "Invalid User ID Length";
                    break;
                case ErrorConstants.ICAMSDK_ERROR_SERVER_DISCONNECTED:
                    strErrorDescription = "Server is Disconnected";
                    break;
                case ErrorConstants.ICAMSDK_ERROR_TRANSACTION_LOG_RECORDS_SIZE:
                    strErrorDescription = "Error in Transaction Log Records Size";
                    break;
                case ErrorConstants.ICAMSDK_SDK_ERROR_IN_RESPONSE_PACKET:
                    strErrorDescription = "Error in Response Packet";
                    break;
                case ErrorConstants.ICAMSDK_ERROR_IN_SECURITYID_REQ_DATA:
                    strErrorDescription = "Error in Security ID Required Data";
                    break;
                case ErrorConstants.ICAMSDK_ERROR_IN_SECURITYID_RESP_DATA:
                    strErrorDescription = "Error in Security ID Response Data";
                    break;
                case ErrorConstants.ICAMSDK_ERROR_IN_LOGIN_REQ_DATA:
                    strErrorDescription = "Error in Login Required Data";
                    break;
                case ErrorConstants.ICAMSDK_ERROR_IN_LOGIN_RESP_DATA:
                    strErrorDescription = "Error in Login Response Data";
                    break;
                case ErrorConstants.ICAMSDK_ERROR_IN_LOGIN_RESP_DATA_SIZE:
                    strErrorDescription = "Error in Login Response Data Size";
                    break;
                case ErrorConstants.ICAMSDK_ERROR_IN_IRISINFO_EYETYPE:
                    strErrorDescription = "Error in IrisInfo EyeType";
                    break;
                case ErrorConstants.ICAMSDK_ERROR_RECOGVERSION_IS_GREATER:
                    strErrorDescription = "Error in Recog Version is Greater";
                    break;
                case ErrorConstants.ICAMSDK_ERROR_SDKVERSION_IS_GREATER:
                    strErrorDescription = "Error in SDK Version is Greater";
                    break;
                case ErrorConstants.ICAMSDK_ERROR_IN_LEFTIRIS_EYETYPE:
                    strErrorDescription = "Error in Left Iris EyeType";
                    break;
                case ErrorConstants.ICAMSDK_ERROR_IN_RIGHTIRIS_EYETYPE:
                    strErrorDescription = "Error in Right Iris EyeType";
                    break;
                case ErrorConstants.ICAMSDK_ERROR_IN_EYETYPE:
                    strErrorDescription = "Error in EyeType";
                    break;
                case ErrorConstants.ICAMSDK_ERROR_IN_USER_ID_CANOT_BE_EMPTY:
                    strErrorDescription = "Error in UserID Cannot be Empty";
                    break;
                case ErrorConstants.ICAMSDK_ERROR_SECURITYID_LENGTH:
                    strErrorDescription = "Error in SecurityID Length";
                    break;
                case ErrorConstants.ICAMSDK_ERROR_GETFIRST_NOT_CALLED:
                    strErrorDescription = "Error in GetTransactionsFirst Not Called";
                    break;
                case ErrorConstants.ICAMSDK_ERROR_IN_DECRYPTION:
                    strErrorDescription = "Error in Decryption";
                    break;
                case ErrorConstants.ICAMSDK_ERROR_USER_RECORDS_SIZE:
                    strErrorDescription = "Error in User Records Size";
                    break;
                case ErrorConstants.ICAMSDK_ERROR_TIMEGROUP_RECORDS_SIZE:
                    strErrorDescription = "Error in TimeGroup Records Size";
                    break;
                case ErrorConstants.ICAMSDK_ERROR_HOLIDAY_RECORDS_SIZE:
                    strErrorDescription = "Error in Holiday Records Size";
                    break;
                case ErrorConstants.ICAMSDK_ERROR_IN_ADD_HOLIDAY_RESP_SIZE:
                    strErrorDescription = "Error in Add Holiday Response Size";
                    break;
                case ErrorConstants.ICAMSDK_ERROR_IN_UPDATE_HOLIDAY_RESP_SIZE:
                    strErrorDescription = "Error in Update Holiday Response Size";
                    break;
                case ErrorConstants.ICAMSDK_ERROR_SYSTEMLOG_RECORDS_SIZE:
                    strErrorDescription = "Error in SystemLog Records Size";
                    break;
                case ErrorConstants.ICAMSDK_ERROR_IN_FRAME_TYPE:
                    strErrorDescription = "Error in Frame Type";
                    break;
                case ErrorConstants.ICAMSDK_ERROR_IN_DATE_SIZE:
                    strErrorDescription = "Error in Data Size";
                    break;
                case ErrorConstants.ICAMSDK_ERROR_OPERATIONLOG_RECORDS_SIZE:
                    strErrorDescription = "Error in OperationLog Records Size";
                    break;
                case ErrorConstants.ICAMSDK_ERROR_INVALID_DATE_TIME:
                    strErrorDescription = "Invalid Date Time";
                    break;
                case ErrorConstants.ICAMSDK_ERROR_INVALID_PIN_NUMBER:
                    strErrorDescription = "Invalid Pin Number";
                    break;
                case ErrorConstants.ICAMSDK_ERROR_END_DATE_IS_LESSTHAN_START_DATE:
                    strErrorDescription = "End Date is Less than/Equal to Start Date";
                    break;
                case ErrorConstants.ICAMSDK_ERROR_IN_IRISIMAGE_DATA:
                    strErrorDescription = "Error in Iris Image Data";
                    break;
                case ErrorConstants.ICAMSDK_ERROR_IRISCODE_DATA_SIZE:
                    strErrorDescription = "Error in Iris Code Data Size";
                    break;
                case ErrorConstants.ICAMSDK_ERROR_IRISCODE_MATCHMODE:
                    strErrorDescription = "Error in Iris Code Match Mode";
                    break;
                case ErrorConstants.ICAMSDK_ERROR_IN_FAKEEYE_LEVEL:
                    strErrorDescription = "Error in Fake Eye Level";
                    break;
                case ErrorConstants.ICAMSDK_ERROR_IN_CAPTURE_TIMEOUT:
                    strErrorDescription = "Error in Capture TimeOut";
                    break;
                case ErrorConstants.ICAMSDK_ERROR_TX_FILTER_DATA:
                    strErrorDescription = "Error in Transaction Log Filter Data";
                    break;
                case ErrorConstants.ICAMSDK_ERROR_IN_IPADDRESS_FORMAT:
                    strErrorDescription = "Error in IP Address Format";
                    break;
                case ErrorConstants.ICAMSDK_ERROR_IN_CONNECTION_OPTIONS:
                    strErrorDescription = "Error in Connection Options";
                    break;
                case ErrorConstants.ICAMSDK_ERROR_QUALITYTYPE_IRIS_IMAGE_NOT_SUPPORTED:
                    strErrorDescription = "Error in Quality type Iris image not supported";
                    break;
                case ErrorConstants.ICAMSDK_ERROR_IN_IRIS_CODE_QUALITY:
                    strErrorDescription = "Error in Iris Code Quality";
                    break;
                case ErrorConstants.ICAMSDK_ERROR_IN_LEFT_IRIS_CODE_QUALITY:
                    strErrorDescription = "Error in Left Iris Code Quality";
                    break;
                case ErrorConstants.ICAMSDK_ERROR_IN_RIGHT_IRIS_CODE_QUALITY:
                    strErrorDescription = "Error in Right Iris Code Quality";
                    break;
                case ErrorConstants.ICAMSDK_ERROR_IN_CARD_TIMEOUT:
                    strErrorDescription = "Card Detect Timeout (1~10 seconds)";
                    break;
                case ErrorConstants.ICAMSDK_ERROR_IN_TIMEOUT:
                    strErrorDescription = "Error in Timeout";
                    break;
                case ErrorConstants.ICAMSDK_ERROR_INVALID_USER_TYPE:
                    strErrorDescription = "Invalid User Type";
                    break;
                case ErrorConstants.ICAMSDK_ERROR_INVALID_DATE_TIME_FOR_USER_TYPE_VISITOR:
                    strErrorDescription = "Invalid Date time for User type Visitor";
                    break;
                case ErrorConstants.ICAMSDK_ERROR_START_DATE_SHOULD_BE_ENTERED:
                    strErrorDescription = "Start Date time should be entered";
                    break;
                case ErrorConstants.ICAMSDK_ERROR_IN_NEW_USER_ID_LENGTH:
                    strErrorDescription = "New UserID cannot be null";
                    break;
                case ErrorConstants.ICAMSDK_ERROR_USER_ID_CANNOT_BE_NULL:
                    strErrorDescription = "User ID cannot be null";
                    break;
                case ErrorConstants.ICAMSDK_ERROR_IN_CARDKIND_PARAMETER:
                    strErrorDescription = "Error in Card Kind Parameter";
                    break;
                case ErrorConstants.ICAMSDK_ERROR_CARD_ID_IS_EMPTY:
                    strErrorDescription = "Card ID is empty";
                    break;
                case ErrorConstants.ICAMSDK_ERROR_CARD_NUMBER_IS_EMPTY:
                    strErrorDescription = "Card Number is empty";
                    break;
                case ErrorConstants.ICAMSDK_ERROR_IN_CARD_ID_LENGTH:
                    strErrorDescription = "Card ID cannot be null";
                    break;
                case ErrorConstants.ICAMSDK_ERROR_IN_CARD_NUMBER_LENGTH:
                    strErrorDescription = "Card Number cannot be null";
                    break;
                case ErrorConstants.ICAMSDK_ERROR_INVALID_END_DATE_TIME_FOR_USER_TYPE_VISITOR:
                    strErrorDescription = "Invalid End date time for Visitor";
                    break;
                case ErrorConstants.ICAMSDK_ERROR_INVALID_START_DATE_TIME_FOR_USER_TYPE_VISITOR:
                    strErrorDescription = "Invalid Start date time for Visitor";
                    break;
                case ErrorConstants.ICAMSDK_ERROR_HOLIDAY_NAME_IS_MANDATORY:
                    strErrorDescription = "Holiday Name is Mandatory";
                    break;
                case ErrorConstants.ICAMSDK_ERROR_TIMEGROUP_NAME_IS_MANDATORY:
                    strErrorDescription = "Time Group Name is Mandatory";
                    break;
                case ErrorConstants.ICAMSDK_ERROR_INVALID_TIMELIST:
                    strErrorDescription = "Invalid Time list";
                    break;
                case ErrorConstants.ICAMSDK_ERROR_TIMEINFO_NOT_SET:
                    strErrorDescription = "Time Info Not Set";
                    break;
                case ErrorConstants.ICAMSDK_ERROR_STARTTIME_SHOULD_BE_LESSTHAN_ENDTIME:
                    strErrorDescription = "Start Time should be less than End time";
                    break;
                case ErrorConstants.ICAMSDK_ERROR_TIMECOUNT_IS_GREATERTHAN_TIMELIST:
                    strErrorDescription = "Start Time count is greater than timelist";
                    break;
                case ErrorConstants.ICAMSDK_ERROR_TIMEINFO_HOURS_SHOULD_NOT_BE_GREATER_THAN_24:
                    strErrorDescription = "TimeInfo hours should not be greater than 24";
                    break;
                case ErrorConstants.ICAMSDK_ERROR_TIMEINFO_MINUTES_SHOULD_NOT_BE_GREATER_THAN_59:
                    strErrorDescription = "TimeInfo minutes should not be greater than 59";
                    break;
                case ErrorConstants.ICAMSDK_ERROR_DEVICENAME_LENGTH:
                    strErrorDescription = "Error in DeviceName length";
                    break;
                case ErrorConstants.ICAMSDK_ERROR_SERVER_ALREADY_CONNECTED:
                    strErrorDescription = "Server Already Connected";
                    break;
                case ErrorConstants.ICAMSDK_ERROR_OLD_AND_NEW_LOGINDETAILS_CANOT_BE_SAME:
                    strErrorDescription = "Old and New LoginDetails cannot be same";
                    break;
                case ErrorConstants.ICAMSDK_ERROR_USERNAME_AND_PASSWORD_CANOT_BE_SAME:
                    strErrorDescription = "UserName and Password cannot be same";
                    break;
                case ErrorConstants.ICAMSDK_ERROR_IN_USERNAME_LENGTH:
                    strErrorDescription = "Error in Username length";
                    break;
                case ErrorConstants.ICAMSDK_ERROR_IN_PASSWORD_LENGTH:
                    strErrorDescription = "Error in Password length";
                    break;
                case ErrorConstants.ICAMSDK_ERROR_ANOTHER_REQUEST_IS_IN_PROGRESS:
                    strErrorDescription = "Another Request is in Progress";
                    break;
                case ErrorConstants.ICAMSDK_ERROR_NOT_CONNECTED_TO_ANY_DEVICE:
                    strErrorDescription = "Not Connected to Any Device";
                    break;
                case ErrorConstants.ICAMSDK_ERROR_IN_MEMORYCREATION:
                    strErrorDescription = "Memory error";
                    break;
                case ErrorConstants.ICAMSDK_ERROR_SOFTWARE_DATE_IS_EXPIRED:
                    strErrorDescription = "iCAM Manager S/W Date Expired";
                    break;
                case ErrorConstants.ICAMSDK_ERROR_IMAGETYPE_NOT_SUPPORTED:
                    strErrorDescription = "Unsupported image type";
                    break;
                case ErrorConstants.ICAMSDK_ERROR_IMAGEWIDTH_NOT_SUPPORTED:
                    strErrorDescription = "Unsupported image width";
                    break;
                case ErrorConstants.ICAMSDK_ERROR_IMAGEHEIGHT_NOT_SUPPORTED:
                    strErrorDescription = "Unsupported image height";
                    break;
                case ErrorConstants.ICAMSDK_ERROR_IN_FACEIMAGE_SIZE:
                    strErrorDescription = "Image size exceeds the maximum limit";
                    break;
                case ErrorConstants.ICAMSDK_ERROR_IN_SYS_CONFIG_DATA:
                    strErrorDescription = "Error in iCAM configuration data";
                    break;
                case ErrorConstants.ICAMSDK_ERROR_SCHEDULE_OVERLAP:
                    strErrorDescription = "Invalid time data. Overlapping time slots found";
                    break;
                case ErrorConstants.ICAMSDK_ERROR_UNKNOWN:
                    strErrorDescription = "Unknown Error";
                    break;
                case ErrorConstants.RECOG70003SDK_ERR_USRID_PWD:
                    strErrorDescription = "Invalid UserName/Password";
                    break;
                case ErrorConstants.RECOG70003SDK_ERR_INCORRECT_SID:
                    strErrorDescription = "Invalid Security ID";
                    break;
                case ErrorConstants.RECOG70003SDK_ERR_FRAME_SIZE:
                    strErrorDescription = "Error In Frame Size";
                    break;
                case ErrorConstants.RECOG70003SDK_ERR_PARAMETER:
                    strErrorDescription = "Error in Parameter";
                    break;
                case ErrorConstants.RECOG70003SDK_ERR_ENCRYPT:
                    strErrorDescription = "Error in Data Ecryption";
                    break;
                case ErrorConstants.RECOG70003SDK_ERR_DECRYPT:
                    strErrorDescription = "Error in Data Decryption";
                    break;
                case ErrorConstants.RECOG70003SDK_ERR_DB:
                    strErrorDescription = "Error in SDK DB";
                    break;
                case ErrorConstants.RECOG70003SDK_ERR_NO_RECORD:
                    strErrorDescription = "No Records";
                    break;
                case ErrorConstants.RECOG70003SDK_ERR_NOT_UNIQUE:
                    strErrorDescription = "Error UserID Not Unique";
                    break;
                case ErrorConstants.RECOG70003SDK_ERR_REQ_FRAME_SIZE_PARAMETER:
                    strErrorDescription = "Error in Frame Size";
                    break;
                case ErrorConstants.RECOG70003SDK_ERR_BLOCK_SIZE_PARAMETER:
                    strErrorDescription = "Error in Block Size";
                    break;
                case ErrorConstants.RECOG70003SDK_ERR_GETFIRST_TOBE_CALLED:
                    strErrorDescription = "Error in  GetTransactionsFirst to be called first";
                    break;
                ////////////////// added for SDK by Praveena 08-02-2012
                case ErrorConstants.RECOG70003SDK_ERROR_USERID_NOT_EXIST:
                    strErrorDescription = "Error UserID not exist";
                    break;
                case ErrorConstants.RECOG70003SDK_ERROR_CARDID_EXIST:
                    strErrorDescription = "Error CardID already exist";
                    break;
                case ErrorConstants.RECOG70003SDK_ERROR_CARD_DETAILS_EXIST:
                    strErrorDescription = "Error Card Details already exist for this user";
                    break;
                case ErrorConstants.RECOG70003SDK_ERROR_DUPLICATE_CARD_RECORDS:
                    strErrorDescription = "Error duplicate card records";
                    break;
                case ErrorConstants.RECOG70003SDK_ERROR_CARD_DETAILS_NOT_EXIST:
                    strErrorDescription = "Error Card Details not exist for this user";
                    break;
                case ErrorConstants.RECOG70003SDK_ERR_IRIS_EXIST:
                    strErrorDescription = "Error Iris Details exist for this user";
                    break;
                case ErrorConstants.RECOG70003SDK_ERR_TGID_IS_INVALID:
                    strErrorDescription = "Time Group ID is Invalid";
                    break;
                case ErrorConstants.RECOG70003SDK_ERR_TG_ALREADY_ASSIGNED:
                    strErrorDescription = "Time Group ID Already assigned";
                    break;
                case ErrorConstants.RECOG70003SDK_ERR_TG_NOT_ASSIGNED:
                    strErrorDescription = "Time Group ID Not Assigned";
                    break;
                case ErrorConstants.RECOG70003SDK_ERR_MAX_TG_ASSIGNED:
                    strErrorDescription = "Max Time Group ID Assigned";
                    break;
                case ErrorConstants.RECOG70003SDK_ERR_FRAME_TYPE:
                    strErrorDescription = "Error Frame Type";
                    break;
                case ErrorConstants.RECOG70003SDK_ERR_SECURITYID_SIZE:
                    strErrorDescription = "Error Security ID Size";
                    break;
                case ErrorConstants.RECOG70003SDK_ERR_USERID_IS_NOT_UNIQUE:
                    strErrorDescription = "User ID is not Unique";
                    break;
                case ErrorConstants.RECOG70003SDK_ERR_PIN_IS_NOT_UNIQUE:
                    strErrorDescription = "Pin Number is not Unique";
                    break;
                case ErrorConstants.RECOG70003SDK_ERR_TG_NAME_NOT_UNIQUE:
                    strErrorDescription = "Time Group Name Not Unique";
                    break;
                case ErrorConstants.RECOG70003SDK_ERR_HOLIDAY_NAME_NOT_UNIQUE:
                    strErrorDescription = "Holiday Name Not Unique";
                    break;
                //////////////////Added for SDK by Swapnil 24-04-2012
                case ErrorConstants.RECOG70003SDK_ERR_CAPTURE_FAILED:
                    strErrorDescription = "Error Capture Failed";
                    break;
                case ErrorConstants.RECOG70003SDK_ERR_DUPLICATE_IRIS_FOUND:
                    strErrorDescription = "Error Iris is already enrolled";
                    break;
                case ErrorConstants.RECOG70003SDK_ERR_TEMPLATE_CREATION:
                    strErrorDescription = "Error in Template Creation due to bad Iris Images.\nPlease try again.";
                    break;
                case ErrorConstants.RECOG70003SDK_ERR_TEMPLATE_ENCRYPTION:
                    strErrorDescription = "Error in Template Encryption";
                    break;
                case ErrorConstants.RECOG70003SDK_ERR_VERIFICATION_FAILED:
                    strErrorDescription = "Verification Failed";
                    break;
                case ErrorConstants.RECOG70003SDK_ERR_ENROLLMENT_IN_PROGRESS:
                    strErrorDescription = "Enrollment is in Progress";
                    break;
                case ErrorConstants.RECOG70003SDK_ERR_ENROLLMENT_NOT_IN_PROGRESS:
                    strErrorDescription = "Enrollment Not in Progress";
                    break;
                case ErrorConstants.RECOG70003SDK_ERR_ENROLLMENT_CANCELLED:
                    strErrorDescription = "Enrollment Cancelled";
                    break;
                case ErrorConstants.RECOG70003SDK_ERR_CMD_NOT_SUPPORTED:
                    strErrorDescription = "This Operation is not Valid in the Current Mode";
                    break;
                case ErrorConstants.RECOG70003SDK_ERR_CLIENT_ALREADY_CONNECTED:
                    strErrorDescription = "Error Client Already Connected" + strIPAddress;
                    break;
                case ErrorConstants.RECOG70003SDK_ERR_USER_LIMIT_REACHED:
                    strErrorDescription = "User Limit Reached";
                    break;
                case ErrorConstants.RECOG70003SDK_ERR_IRISCODE_LIMIT_REACHED:
                    strErrorDescription = "Error Iris Code Limit Reached";
                    break;
                case ErrorConstants.RECOG70003SDK_ERR_INVALID_FILTER_DATA:
                    strErrorDescription = "Invalid Filter Data";
                    break;
                case ErrorConstants.RECOG70003SDK_ERR_IN_MATCH_MODE:
                    strErrorDescription = "Error in Match Mode";
                    break;
                case ErrorConstants.RECOG70003SDK_ERR_INVALID_FILTER_TYPE:
                    strErrorDescription = "Invalid Filter Type";
                    break;
                case ErrorConstants.RECOG70003SDK_ERR_INVALID_CONNECTION_TYPE:
                    strErrorDescription = "Invalid Connection Type";
                    break;
                case ErrorConstants.RECOG70003SDK_ERR_ACCEPTED_RO_CONNECTION:
                    strErrorDescription = "Error Accepted ReadOnly Connection";
                    break;
                case ErrorConstants.RECOG7000SDK_ERR_GET_SYS_INFO:
                    strErrorDescription = "Error in Getting System Information";
                    break;
                case ErrorConstants.RECOG7000SDK_ERR_POOR_IRIS_CODE_QUALITY:
                    strErrorDescription = "Poor Iris Code Quality";
                    break;
                case ErrorConstants.RECOG7000SDK_ERR_POOR_IRIS_IMAGE_QUALITY:
                    strErrorDescription = "Poor Iris Image Quality";
                    break;
                case ErrorConstants.RECOG7000SDK_ERR_MAX_TG_LIMIT_REACHED:
                    strErrorDescription = "Time Group Limit Reached";
                    break;
                case ErrorConstants.RECOG7000SDK_ERR_MAX_SCHEDULE_LIMIT_REACHED:
                    strErrorDescription = "Max Schedule Limit Reached";
                    break;
                case ErrorConstants.RECOG7000SDK_ERR_MAX_HOLIDAY_LIMIT_REACHED:
                    strErrorDescription = "Holidays Limit Reached";
                    break;
                case ErrorConstants.RECOG70003SDK_ERR_ALREADYIN_ENROLLMODE:
                    strErrorDescription = "Already in Enroll Mode";
                    break;
                case ErrorConstants.RECOG70003SDK_ERR_ALREADYIN_RECOGNITION:
                    strErrorDescription = "Already in Recognition Mode";
                    break;
                case ErrorConstants.RECOG70003SDK_ERR_DEVICE_IS_NOT_IN_ENROLLMODE:
                    strErrorDescription = "Device is Not in Enroll Mode";
                    break;
                case ErrorConstants.RECOG70003SDK_ERR_CARD_DETECTION_NOT_STARTED:
                    strErrorDescription = "Card Detection Not Started";
                    break;
                case ErrorConstants.RECOG70003SDK_ERR_CMD_NOT_VALID_FOR_ENROLL_MODE:
                    strErrorDescription = "Command not valid for Enroll Mode";
                    break;
                case ErrorConstants.RECOG70003SDK_ERR_CMD_NOT_VALID_FOR_RECOG_MODE:
                    strErrorDescription = "Command not valid for Recognition Mode";
                    break;
                case ErrorConstants.RECOG70003SDK_ERR_CARD_DETECTION_ALREADY_STARTED:
                    strErrorDescription = "Card Detection Already Started";
                    break;
                case ErrorConstants.RECOG70003SDK_ERR_MODE_IS_NOT_CONFIG_TO_SMARTCARD:
                    strErrorDescription = "Recognition mode Not Configured in SmartCard mode or 'Iris or Card' with 'Use as Prox Card' selected";
                    break;
                case ErrorConstants.RECOG70003SDK_ERR_CARD_DETECTION_IS_INPROGRESS:
                    strErrorDescription = "Card Detection is in Progress";
                    break;
                case ErrorConstants.RECOG70003SDK_ERR_USER_LOGIN_DETAILS_INVALID:
                    strErrorDescription = "Old UserName/Password Is Invalid, Can't Update Login Details";
                    break;
                case ErrorConstants.RECOG70003SDK_ERR_FAILED_TO_UPDATE_LOGINDETAILS:
                    strErrorDescription = "Failed to update Login Details.";
                    break;
                case ErrorConstants.RECOG70003SDK_ERR_IRIS_FOUND_FOR_DIFF_USER:
                    strErrorDescription = "Iris is already enrolled with different User.";
                    break;
                case ErrorConstants.RECOG70003SDK_ERR_FIRSTIRISTEMPLATE_CREATION_NOT_ALLOWED:
                    strErrorDescription = "License Limit reached Can't create new iris templates.";
                    break;
                case ErrorConstants.RECOG70003SDK_ERR_IRIS_EXIST_BUT_NOT_MATCHED:
                    strErrorDescription = "Captured Iris not Matched with existing Iris.";
                    break;
                case ErrorConstants.RECOG70003SDK_ERR_FACE_DETAILS_EXIST:
                    strErrorDescription = "Face image data alreday exists.";
                    break;
                case ErrorConstants.RECOG70003SDK_ERR_FACE_DETAILS_NOT_EXIST:
                    strErrorDescription = "Face image data does not exist.";
                    break;
                case ErrorConstants.RECOG70003SDK_ERR_FACEIMAGE_SIZE:
                    strErrorDescription = "Error in face image data size.";
                    break;
                case ErrorConstants.RECOG70003SDK_ERR_CANT_CREATE_FACEIMAGE:
                    strErrorDescription = "Failed to create face image data.";
                    break;
                case ErrorConstants.RECOG70003SDK_ERR_CANT_READ_FACEIMAGE:
                    strErrorDescription = "Failed to read face image data.";
                    break;
                case ErrorConstants.RECOG70003SDK_ERR_FACEIAMGE_DELETE_FAILED:
                    strErrorDescription = "Failed to delete face image data.";
                    break;
                case ErrorConstants.IASOCKET_ERROR_CONNECT_SERVER:
                    strErrorDescription = "Error in Connecting to Server" + strIPAddress;
                    break;
                case ErrorConstants.IASOCKET_ERROR_DISCONNECT_SERVER:
                    strErrorDescription = "Error in Disconnecting From Server" + strIPAddress;
                    break;
                case ErrorConstants.IASOCKET_ERROR_OPEN:
                    strErrorDescription = "Error in Opening Socket" + strIPAddress;
                    break;
                case ErrorConstants.IASOCKET_ERROR_PARAMETER:
                    strErrorDescription = "Error in Socket Parameter";
                    break;
                case ErrorConstants.IASOCKET_ERROR_RECEIVE:
                    strErrorDescription = "Error in Socket Receive";
                    break;
                case ErrorConstants.IASOCKET_ERROR_RESPOND:
                    strErrorDescription = "Error in Socket Response";
                    break;
                case ErrorConstants.IASOCKET_ERROR_SEND:
                    strErrorDescription = "Error in Socket Send" + strIPAddress;
                    break;
                case ErrorConstants.IASOCKET_ERROR_DATA_SIZE:
                    strErrorDescription = "Error in Socket Data Size";
                    break;
                case ErrorConstants.IASOCKET_ERROR_EVENT_TIME_OUT:
                    strErrorDescription = "Error in Socket Event Time Out";
                    break;
                case ErrorConstants.IASOCKET_ERROR_MEMORY:
                    strErrorDescription = "Error in Socket Memory";
                    break;

                case ErrorConstants.IASOCKET_ERROR_UNKNOWN:
                    strErrorDescription = "Unknown Error in Socket";
                    break;
                //Added by Swapnil on 01st March 2012 IAClientDBDefs
                case ErrorConstants.IACDB_ADD_SKIPPED_MAXLIMIT:
                    strErrorDescription = "DB Add Skipped MaxLimit";
                    break;
                case ErrorConstants.IACDB_ERROR_CHECKSUM:
                    strErrorDescription = "DB Error CheckSum";
                    break;
                case ErrorConstants.IACDB_ERROR_CLOSE:
                    strErrorDescription = "DB Error Close";
                    break;
                case ErrorConstants.IACDB_ERROR_CONNECTION:
                    strErrorDescription = "DB Error Connection";
                    break;
                case ErrorConstants.IACDB_ERROR_CREATE_LIST:
                    strErrorDescription = "DB Error Create List";
                    break;
                case ErrorConstants.IACDB_ERROR_DATE:
                    strErrorDescription = "DB Error Date";
                    break;
                case ErrorConstants.IACDB_ERROR_MANDATORY:
                    strErrorDescription = "DB Error Mandatory";
                    break;
                case ErrorConstants.IACDB_ERROR_MEMORY:
                    strErrorDescription = "DB Error Memory";
                    break;
                case ErrorConstants.IACDB_ERROR_NO_RECORD:
                    strErrorDescription = "No Records";
                    break;
                case ErrorConstants.IACDB_ERROR_NOT_DESELECT:
                    strErrorDescription = "DB Error Not DeSelect";
                    break;
                case ErrorConstants.IACDB_ERROR_NOT_OPEN:
                    strErrorDescription = "DB Error Not Open";
                    break;
                case ErrorConstants.IACDB_ERROR_NOT_SELECT:
                    strErrorDescription = "DB Error Not Select";
                    break;
                case ErrorConstants.IACDB_ERROR_NOT_UNIQUE:
                    strErrorDescription = "DB Error Not Unique";
                    break;
                case ErrorConstants.IACDB_ERROR_OPEN:
                    strErrorDescription = "DB Error Open";
                    break;
                case ErrorConstants.IACDB_ERROR_PARAMETER:
                    strErrorDescription = "DB Error in Parameter";
                    break;
                case ErrorConstants.IACDB_ERROR_QUERY:
                    strErrorDescription = "DB Error in Query";
                    break;
                case ErrorConstants.IACDB_ERROR_RECORDSET:
                    strErrorDescription = "DB Error in RecordSet";
                    break;
                case ErrorConstants.IACDB_ERROR_SIZE:
                    strErrorDescription = "DB Error in Size";
                    break;
                case ErrorConstants.IACDB_ERROR_UNKNOWN:
                    strErrorDescription = "DB Error Unknown";
                    break;
                case ErrorConstants.ICAMSDK_ERROR_CONNECTION_LIMIT_REACHED:
                    strErrorDescription = "SDK Connection Limit Is Reached";
                    break;
                case ErrorConstants.ICAMSDK_ERROR_USER_LIMIT_REACHED:
                    strErrorDescription = "Gallery Size Is Reached Can't Add Users";
                    break;
                case ErrorConstants.ICAMSDK_ERROR_INVALID_GALLERY_SIZE:
                    strErrorDescription = "Device Has More User Records Than The Licensed Gallery Size";
                    break;
                case ErrorConstants.ICAMSDK_ERROR_IN_LICENSE_INITIALIZE:
                    strErrorDescription = "Error In License Initialization";
                    break;
                case ErrorConstants.RECOG70003SDK_ERR_FACECAPTURE_IN_PROGRESS:
                    strErrorDescription = "Face capture is in progress";
                    break;
                case ErrorConstants.RECOG70003SDK_ERR_FACEIMAGE_TYPE:
                    strErrorDescription = "Invalid face image type";
                    break;
                case ErrorConstants.RECOG70003SDK_ERR_FACECAPTURE_CANCELLED:
                    strErrorDescription = "Face capture cancelled";
                    break;
                case ErrorConstants.RECOG70003SDK_ERR_FACECAPTURE_NOT_IN_PROGRESS:
                    strErrorDescription = "Face capture is not in progress";
                    break;
                case ErrorConstants.RECOG70003SDK_ERR_FACECAPTURE_IMAGETYPE:
                    strErrorDescription = "Invalid face capture image type";
                    break;
                case ErrorConstants.RECOG70003SDK_ERR_UNKNOWN:
                    strErrorDescription = "Unknown error";
                    break;
                case ErrorConstants.RECOG70003SDK_ERR_ONDEVICEENROLL_INPROGRESS:
                    strErrorDescription = "On device enrollment is in progress";
                    break;
                case ErrorConstants.RECOG7000SDK_ERR_GET_SYS_CONFIG:
                    strErrorDescription = "Error in getting iCAM configuration";
                    break;
                case ErrorConstants.RECOG7000SDK_ERR_SET_SYS_CONFIG:
                    strErrorDescription = "Error in setting iCAM configuration";
                    break;
                case ErrorConstants.RECOG7000SDK_ERR_HOLIDAY_EXISTS:
                    strErrorDescription = "Holiday already exists";
                    break;
                default:
                    strErrorDescription = "Unknown error :" + iErrorCode;
                    break;
            }

            return strErrorDescription;
        }

        private static string GetLicenseError(long iErrorCode)
        {
            string strErrorDescription;

            switch (iErrorCode)
            {
                case ErrorConstants.ERR_LFW_FLXERROR_CREATE:
                    strErrorDescription = "Failed to create/update license terms.";
                    break;
                case ErrorConstants.ERR_LFW_XMLFILE_READ:
                    strErrorDescription = "Failed to read license file.";
                    break;
                case ErrorConstants.ERR_LFW_DATACONVERSION:
                    strErrorDescription = "Failed to convert license data.";
                    break;
                case ErrorConstants.ERR_LFW_STOREPATH:
                    strErrorDescription = "Failed to read License Details.";
                    break;
                case ErrorConstants.ERR_LFW_SAVETRIAL:
                    strErrorDescription = "Failed to Save Trial License.";
                    break;
                case ErrorConstants.ERR_LFW_WINDBACK:
                    strErrorDescription = "System Date is set to Back Date.";
                    break;
                case ErrorConstants.ERR_LFW_COPY_FILE:
                    strErrorDescription = "Failed to copy the lincense File.";
                    break;
                case ErrorConstants.ERR_LFW_PARAMETER:
                    strErrorDescription = "License parameter error.";
                    break;
                case ErrorConstants.ERR_LFW_METER:
                    strErrorDescription = "License limit is reached or license data is corrupted.";
                    break;
                case ErrorConstants.ERR_LFW_LICENSE_IS_INUSE:
                    strErrorDescription = "Licnese is already in Use.";
                    break;
                case ErrorConstants.ERR_LFW_FILEOPEN:
                    strErrorDescription = "Failed to Intialize the License.";
                    break;
                case ErrorConstants.ERR_LFW_TRIAL_ALREADY_LOADED:
                    strErrorDescription = "Trial License is Already Loaded.";
                    break;
                case ErrorConstants.ERR_LFW_TRIAL_EXPIRED:
                    strErrorDescription = "Trial License is Expired.";
                    break;
                case ErrorConstants.ERR_LFW_TRIAL_INVALID_ID:
                    strErrorDescription = "Invalid Trial License.";
                    break;
                case ErrorConstants.ERR_LFW_FEATURE_VERSION_NOT_FOUND:
                    strErrorDescription = "Feature vesrion not found.";
                    break;
                case ErrorConstants.ERR_LFW_FEATURE_NOT_STARTED:
                    strErrorDescription = "Feature not started.";
                    break;
                case ErrorConstants.ERR_LFW_DATE_INVALID:
                    strErrorDescription = "Date is invalid.";
                    break;
                case ErrorConstants.ERR_LFW_FEATURE_EXPIRED:
                    strErrorDescription = "Feature is Expired.";
                    break;
                case ErrorConstants.ERR_LFW_LICENSE_NOT_FOUND:
                    strErrorDescription = "License Not Found.";
                    break;
                case ErrorConstants.ERR_LFW_LICENSE_SOURCE_TYPE_INVALID:
                    strErrorDescription = "License Source Type is invalid.";
                    break;
                case ErrorConstants.ERR_LFW_WINDBACK_DETECTED:
                    strErrorDescription = "System Date is set to Back Date.";
                    break;
                case ErrorConstants.ERR_LFW_FEATURE_INSUFFICIENT_COUNT:
                    strErrorDescription = "Feature count not available in licnese.";
                    break;
                case ErrorConstants.ERR_LFW_HOSTID_INVALID:
                    strErrorDescription = "Host ID is invalid.";
                    break;
                case ErrorConstants.ERR_LFW_DATA_CORRUPTED:
                    strErrorDescription = "License Data corrupted.";
                    break;
                case ErrorConstants.ERR_LFW_RESPONSE_INVALID:
                    strErrorDescription = "License Response Data is Invalid.";
                    break;
                case ErrorConstants.ERR_LFW_FEATURE_INVALID:
                    strErrorDescription = "Feature is Invalid.";
                    break;
                case ErrorConstants.ERR_LFW_TS_CORRUPTED:
                    strErrorDescription = "License Store is Corrupted.";
                    break;
                case ErrorConstants.ERR_LFW_IDENTITY_INVALID:
                    strErrorDescription = "Identity is Invalid.";
                    break;
                case ErrorConstants.ERR_LFW_VENDORKEY_INVALID:
                    strErrorDescription = "Vendor Key is Invalid.";
                    break;
                case ErrorConstants.ERR_LFW_SIGNATURE_INVALID:
                    strErrorDescription = "Signature is Invalid.";
                    break;
                case ErrorConstants.ERR_LFW_PUBLISHER_DATA_NOT_SET:
                    strErrorDescription = "Publisher Data Not set.";
                    break;
                case ErrorConstants.ERR_LFW_FEATURE_HOST_ID_MISMATCH:
                    strErrorDescription = "Feature HostID not Matching with System HostID.";
                    break;
                case ErrorConstants.ERR_LFW_TS_HOST_ID_MISMATCH:
                    strErrorDescription = "HostID Mismatch in Trusted Store";
                    break;
                case ErrorConstants.ERR_LFW_FEATURE_NOT_FOUND:
                    strErrorDescription = "License Feature Not Found.";
                    break;
                case ErrorConstants.ERR_LFW_TS_ANCHOR_BREAK:
                    strErrorDescription = "License Store Is broken.";
                    break;
                case ErrorConstants.ERR_LFW_TS_BINDING_BREAK:
                    strErrorDescription = "License Binding Failed.";
                    break;
                case ErrorConstants.ERR_LFW_TS_DOES_NOT_EXIST:
                    strErrorDescription = "License Store not Exist.";
                    break;
                case ErrorConstants.ERR_LFW_IDENTITY_UNSUPPORTED_VERSION:
                    strErrorDescription = "Identity Version Unsupported.";
                    break;
                case ErrorConstants.ERR_LFW_VENDOR_KEYS_EXPIRED:
                    strErrorDescription = "Vendor Keys Expired";
                    break;
                case ErrorConstants.ERR_LFW_VENDOR_KEYS_NOT_ENABLED:
                    strErrorDescription = "Vendor Keys Not Active";
                    break;
                case ErrorConstants.ERR_LFW_ITEM_NOT_FOUND:
                    strErrorDescription = "License Item Not Found.";
                    break;
                case ErrorConstants.ERR_LFW_INDEX_OUT_OF_BOUND:
                    strErrorDescription = "Metered License Index not Matching";
                    break;
                case ErrorConstants.ERR_LFW_FEATURE_SERVER_HOST_ID_MISMATCH:
                    strErrorDescription = "Feature HostID not Matching with System HostID.";
                    break;
                case ErrorConstants.ERR_LFW_VERSION_STRING_INVALID:
                    strErrorDescription = "Version String is Invalid";
                    break;
                case ErrorConstants.ERR_LFW_LICENSE_ERROR:
                    strErrorDescription = " License Error.";
                    break;

                default:
                    strErrorDescription = "License Error " + iErrorCode;
                    break;
            }

            return strErrorDescription;
        }
    }
}
