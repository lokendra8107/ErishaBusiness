using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ErishaBusiness.Core
{
    #region [START : MESSAGETYPE]
    public enum MessageType
    {
        Warning,
        Success,
        Danger,
        Info
    }
    #endregion

    #region [START : EMAIL TEMPLATES]
    public enum EmailTemplates
    {
        Warning,
        Success,
        Danger,
        Info
    }
    #endregion

    #region [START : USERROLES]
    public struct UserRoles
    {
        public const string Admin = "Admin";
        public const string User = "Doctor";
    }
    #endregion

    #region [START : Role Type]
    public enum RoleType
    {
        [Display(Name = "Admin")]
        Admin = 1,
        [Display(Name = "User")]
        User = 2
    }
    #endregion

    #region [START : Publish Status]
    public enum PublishStatus
    {
        [Display(Name = "Yes")]
        Yes = 1,
        [Display(Name = "No")]
        No = 2
    }
    #endregion

    #region [START : Genre Type]
    public enum Genres
    {
        [Display(Name = "Rock Pop")]
        RockPop = 1,
        [Display(Name = "Gospel")]
        Gospel = 2,
        [Display(Name = "Jazz & Blues")]
        JazzBlues = 3,
        [Display(Name = "R & B Hip Hop")]
        RHipHop = 4,
        [Display(Name = "Opera")]
        Opera = 5,
        [Display(Name = "Theatre Stage")]
        TheatreStage = 6
    }
    #endregion

    #region [START : Levels Type]
    public enum Levels
    {
        [Display(Name = "Beg/Int")]
        BegIntLevel = 1,
        [Display(Name = "Int/Adv")]
        IntAdvLevel = 2,
        [Display(Name = "Adv/Pro")]
        AdvProLevel = 3
    }
    #endregion

    #region [START : Duration]
    public enum DurationWeek
    {
        [Display(Name = "1")]
        Week1 = 1,
        [Display(Name = "2")]
        Week2 = 2,
        [Display(Name = "3")]
        Week3 = 3,
        [Display(Name = "4")]
        Week4 = 4,
        [Display(Name = "5")]
        Week5 = 5,
        [Display(Name = "6")]
        Week6 = 6,
        [Display(Name = "7")]
        Week7 = 7,
        [Display(Name = "8")]
        Week8 = 8,
        [Display(Name = "9")]
        Week9 = 9,
        [Display(Name = "10")]
        Week10 = 10,
        [Display(Name = "11")]
        Week11 = 11,
        [Display(Name = "12")]
        Week12 = 12,
        [Display(Name = "13")]
        Week13 = 13,
        [Display(Name = "14")]
        Week14 = 14,
        [Display(Name = "15")]
        Week15 = 15,
        [Display(Name = "16")]
        Week16 = 16,
        [Display(Name = "17")]
        Week17 = 17,
        [Display(Name = "18")]
        Week18 = 18,
        [Display(Name = "19")]
        Week19 = 19,
        [Display(Name = "20")]
        Week20 = 20,
        [Display(Name = "21")]
        Week21 = 21,
        [Display(Name = "22")]
        Week22 = 22,
        [Display(Name = "23")]
        Week23 = 23,
        [Display(Name = "24")]
        Week24 = 24,
        [Display(Name = "25")]
        Week25 = 25,
        [Display(Name = "26")]
        Week26 = 26,
        [Display(Name = "27")]
        Week27 = 27,
        [Display(Name = "28")]
        Week28 = 28,
        [Display(Name = "29")]
        Week29 = 29,
        [Display(Name = "30")]
        Week30 = 30,
        [Display(Name = "31")]
        Week31 = 31,
        [Display(Name = "32")]
        Week32 = 32,
        [Display(Name = "33")]
        Week33 = 33,
        [Display(Name = "34")]
        Week34 = 34,
        [Display(Name = "35")]
        Week35 = 35,
        [Display(Name = "36")]
        Week36 = 36,
        [Display(Name = "37")]
        Week37 = 37,
        [Display(Name = "38")]
        Week38 = 38,
        [Display(Name = "39")]
        Week39 = 39,
        [Display(Name = "40")]
        Week40 = 40,
        [Display(Name = "41")]
        Week41 = 41,
        [Display(Name = "42")]
        Week42 = 42,
        [Display(Name = "43")]
        Week43 = 43,
        [Display(Name = "44")]
        Week44 = 44,
        [Display(Name = "45")]
        Week45 = 45,
        [Display(Name = "46")]
        Week46 = 46,
        [Display(Name = "47")]
        Week47 = 47,
        [Display(Name = "48")]
        Week48 = 48,
        [Display(Name = "49")]
        Week49 = 49,
        [Display(Name = "50")]
        Week50 = 50,
        [Display(Name = "51")]
        Week51 = 51,
        [Display(Name = "52")]
        Week52 = 52
    }
    #endregion

    #region [START : Duration Minute]
    public enum DurationMinute
    {
        [Display(Name = "30 Minute")]
        Minute30 = 1,
        [Display(Name = "45 Minute")]
        Minute45 = 2,
        [Display(Name = "60 Minute")]
        Minute60 = 3
    }
    #endregion

    #region [START : BloodGroup]
    public enum BloodGroup
    {
        [Display(Name = "A+")]
        AP = 1,
        [Display(Name = "A-")]
        AN = 2,
        [Display(Name = "B+")]
        BP = 3,
        [Display(Name = "B-")]
        BN = 4,
        [Display(Name = "O+")]
        OP = 5,
        [Display(Name = "O-")]
        ON = 6,
        [Display(Name = "AB+")]
        ABP = 7,
        [Display(Name = "AB-")]
        ABN = 8
    }
    #endregion

    #region [START : DOBMonth]
    public enum DOBMonth
    {
        [Display(Name = "Jan")]
        Month1 = 1,
        [Display(Name = "Feb")]
        Month2 = 2,
        [Display(Name = "Mar")]
        Month3 = 3,
        [Display(Name = "Apr")]
        Month4 = 4,
        [Display(Name = "May")]
        Month5 = 5,
        [Display(Name = "Jun")]
        Month6 = 6,
        [Display(Name = "July")]
        Month7 = 7,
        [Display(Name = "Aug")]
        Month8 = 8,
        [Display(Name = "Sep")]
        Month9 = 9,
        [Display(Name = "Oct")]
        Month10 = 10,
        [Display(Name = "Nov")]
        Month11 = 11,
        [Display(Name = "Dec")]
        Month12 = 12
    }
    #endregion

    #region [START : DOBYear]
    public enum DOBYear
    {
        [Display(Name = "1980")]
        Year1980 = 1980,
        [Display(Name = "1981")]
        Year1981 = 1981,
        [Display(Name = "1982")]
        Year1982 = 1982,
        [Display(Name = "1983")]
        Year1983 = 1983,
        [Display(Name = "1984")]
        Year1984 = 1984,
        [Display(Name = "1985")]
        Year1985 = 1985,
        [Display(Name = "1986")]
        Year1986 = 1986,
        [Display(Name = "1987")]
        Year1987 = 1987,
        [Display(Name = "1988")]
        Year1988 = 1988,
        [Display(Name = "1989")]
        Year1989 = 1989,
        [Display(Name = "1990")]
        Year1990 = 1990,
        [Display(Name = "1991")]
        Year1991 = 1991,
        [Display(Name = "1982")]
        Year1992 = 1992,
        [Display(Name = "1993")]
        Year1993 = 1993,
        [Display(Name = "1994")]
        Year1994 = 1994,
        [Display(Name = "1995")]
        Year1995 = 1995,
        [Display(Name = "1996")]
        Year1996 = 1996,
        [Display(Name = "1997")]
        Year1997 = 1997,
        [Display(Name = "1999")]
        Year1998 = 1998,
        [Display(Name = "1989")]
        Year1999 = 1999,
        [Display(Name = "2000")]
        Year2000 = 2000,
        [Display(Name = "2001")]
        Year2001 = 2001,
        [Display(Name = "2002")]
        Year2002 = 2002,
        [Display(Name = "2003")]
        Year2003 = 2003,
        [Display(Name = "2004")]
        Year2004 = 2004,
        [Display(Name = "2005")]
        Year2005 = 2005,
        [Display(Name = "2006")]
        Year2006 = 2006,
        [Display(Name = "2007")]
        Year2007 = 2007,
        [Display(Name = "2008")]
        Year2008 = 2008,
        [Display(Name = "2009")]
        Year2009 = 2009,
        [Display(Name = "2010")]
        Year2010 = 2010,
        [Display(Name = "2011")]
        Year2011 = 2011,
        [Display(Name = "2012")]
        Year2012 = 2012,
        [Display(Name = "2003")]
        Year2013 = 2013,
        [Display(Name = "2014")]
        Year2014 = 2014,
        [Display(Name = "2015")]
        Year2015 = 2015,
        [Display(Name = "2016")]
        Year2016 = 2006,
        [Display(Name = "2017")]
        Year2017 = 2017,
        [Display(Name = "2018")]
        Year2018 = 2018,
        [Display(Name = "2019")]
        Year2019 = 2019
    }
    #endregion

    #region [START : Email Type]
    public enum EmailType
    {
        [Display(Name = "Admin Forgot Password")]
        AdminForgotPassword = 1,
        [Display(Name = "Order Purchase")]
        OrderPurchase = 2,
        [Display(Name = "Registration")]
        Register = 3,
        [Display(Name = "Forgot Password")]
        ForgotPassword = 4,
        [Display(Name = "Order Purchase Admin")]
        OrderPurchaseAdmin = 5,
        [Display(Name = "Order Cancellation")]
        CancelOrder = 6,
        [Display(Name = "Return Request")]
        ReturnRequest = 7,
        [Display(Name = "Admin Order Cancellation")]
        AdminCancelOrder = 8,
        [Display(Name = "Admin Return Request")]
        AdminReturnRequest = 9,
        [Display(Name = "Order Review")]
        OrderReview = 10,
        [Display(Name = "Order Review")]
        AdminOrderReview = 11,
        [Display(Name = "Contact Us")]
        ContactUs = 12,
        [Display(Name = "Order Approval")]
        AdminOrderApproval = 13,
        [Display(Name = "Order Delivered")]
        AdminOrderDelivered = 14
    }
    #endregion

    #region [START : Product Status Type]
    public enum ProductStatusType
    {
        [Display(Name = "In-Progress")]
        InProgress = 1,
        [Display(Name = "Approved")]
        Approved = 2,
        [Display(Name = "Delivered")]
        Delivered = 3,
        [Display(Name = "Cancelled")]
        Cancelled = 4
    }
    #endregion

    #region[START : IMAGE TYPES]
    public enum UploadImageTypes
    {
        [Description("200X200")]
        UserProfile,
        [Description("100X100")]
        Logo,
        [Description("10X10")]
        Fevicon,
        [Description("1200X1200")]
        Big,
        [Description("700X700")]
        Medium,
        [Description("500X500")]
        Small,
    }
    #endregion

    #region[START : INSTRUCTOR DOCUMENT TYPES]
    public enum InstructorDocType
    {
        [Description("Passport")]
        Passport = 1,
        [Description("Driving License")]
        DrivingLicense = 2,
        [Description("Qualification")]
        Qualification = 3,
        [Description("Music Licence")]
        MusicLicence = 4,
        [Description("DBS ")]
        DBS = 5,
        [Description("Qualification_2")]
        Qualification_2 = 6,
        [Description("Qualification_3")]
        Qualification_3 = 7,
        [Description("Insurance")]
        Insurance = 8
    }
    #endregion

    #region [START : QUALIFICATION TYPE]
    public enum QualificationType
    {
        [Description("PT Level 2 – Group Exercise Instructor")]
        PTLevel2GroupExerciseInstructor,
        [Description("PT level 3 – Personal Training")]
        PTlevel3PersonalTraining,
        [Description("PT level 4 ")]
        PTlevel4,
        [Description("Pilates Level 1")]
        PilatesLevel1,
        [Description("Pilates Level 2")]
        PilatesLevel2,
        [Description("Pilates Level 3")]
        PilatesLevel3,
        [Description("Pilates Level 4")]
        PilatesLevel4,

        [Description("APPI Pilates Level 1")]
        APPIPilatesLevel1,
        [Description("APPI Pilates Level 2")]
        APPIPilatesLevel2,
        [Description("APPI Pilates Level 3")]
        APPIPilatesLevel3,
        [Description("APPI Pilates Level 4")]
        APPIPilatesLevel4,

        [Description("Yoga Level 3 Certificate")]
        YogaLevel2Certificate,
        [Description("Yoga Level 3 Diploma")]
        YogaLevel3Certificate,
        [Description("Yoga Level 4 Diploma")]
        YogaLevel4Certificate,
    }
    #endregion

    #region[START : CLASS TIME TYPES]
    public enum ClassTimeType
    {
        [Description("Morning")]
        Morning = 1,
        [Description("Afternoon")]
        Afternoon = 2,
        [Description("Evening")]
        Evening = 3,
        [Description("Night")]
        Night = 4

    }
    #endregion

    #region[START : CONTENT PAGE TYPES]
    public enum ContentPageTypes
    {
        [Description("Terms and Conditions")]
        TermsConditions = 1,
        [Description("Privacy Policy")]
        PrivacyPolicy = 2,
        [Description("About Us")]
        AboutUs = 3,
        [Description("Contact Us")]
        ContactUs = 4,
        [Description("Shipping Policy")]
        ShippingPolicy = 5
    }
    #endregion

    #region[START : MODAL SIZE]
    public enum ModalSize
    {
        Small,
        Large,
        Medium
    }
    #endregion

    #region [START : CANCELLATION POLICY]
    public struct CancellationPolicy
    {
        public const string ClassCancellationPolicy = "Cancel up until 24 hours before the session start time and get a full refund. If you cancel after this time, then the full fee is payable.";
    }
    #endregion

    #region [START : SESSION STATUS]
    public enum SessionStatus
    {
        [Description("None")]
        None = 1,
        [Description("Scheduled")]
        Scheduled = 2,
        [Description("Completed")]
        Completed = 3,
        [Description("Cancelled")]
        Cancelled = 4,
        [Description("Rescheduled")]
        Rescheduled = 5,
    }
    #endregion

    #region [START : PAYMENT STATUS]
    public enum PaymentStatus
    {
        [Description("Pending")]
        Pending = 1,
        [Description("Authorized")]
        Authorized = 2,
        [Description("Paid")]
        Paid = 3,
        [Description("Partially Refunded")]
        PartiallyRefunded = 4,
        [Description("Refunded")]
        Refunded = 5,
        [Description("Voided")]
        Voided = 6,
        [Description("Failed")]
        Failed = 7
    }
    #endregion

    public struct DateTimeFormats
    {
        public const string DateWithTime = "dd MMM yyyy hh:mm tt";
        public const string OnlyDate = "dd MMM yyyy";
        public const string OnlyTime = "hh:mm tt";
    }

    public enum BannerLayouts
    {
        [Description("First Layout")]
        FirstLayout = 1,
        [Description("Second Layout")]
        SecondLayout = 2,
        [Description("PThird Layout")]
        ThirdLayout = 3,
        [Description("Fourth Layout")]
        FourthLayout = 4
    }
}
