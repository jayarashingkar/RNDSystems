using System;
using System.Collections.Generic;
using System.Web.Mvc;

namespace RNDSystems.Models
{
    /// <summary>
    /// Processing column details
    /// </summary>
    public class RNDProcessing
    {
        public int RecID { get; set; }

        ////[StringLength(10)]
        public string WorkStudyID { get; set; }

        public int MillLotNo { get; set; }

        public List<SelectListItem> ddMillLotNo { get; set; }
        ////[StringLength(10)]
        public string Sonum { get; set; }

        public byte ProcessNo { get; set; }

        ////[StringLength(10)]
        public string ProcessID { get; set; }

        public int HTLogNo { get; set; }

        ////[StringLength(10)]
        public string HTLogID { get; set; }

        public int AgeLotNo { get; set; }

        ////[StringLength(10)]
        public string AgeLotID { get; set; }

        //[StringLength(DataLengthConstant.LENGTH_KEY)]
        public string Hole { get; set; }
        public List<SelectListItem> ddHole { get; set; }


        //[StringLength(DataLengthConstant.LENGTH_KEY)]
        public string PieceNo { get; set; }
        public List<SelectListItem> ddPieceNo { get; set; }
        
        //[StringLength(5)]
        public string SHTTemp { get; set; }

        //[StringLength(DataLengthConstant.LENGTH_KEY)]
        public string SHSoakHrs { get; set; }
        
        //[StringLength(DataLengthConstant.LENGTH_KEY)]
        public string SHSoakMns { get; set; }        

        //[StringLength(DataLengthConstant.LENGTH_KEY)]
        public string SHTStartHrs { get; set; }
        public List<SelectListItem> SHTStartHours { get; set; }

        //[StringLength(DataLengthConstant.LENGTH_KEY)]
        public string SHTStartMns { get; set; }
        public List<SelectListItem> SHTStartMinutes { get; set; }

        //public DateTime? SHTDate { get; set; }
        public string SHTDate { get; set; }

        //[StringLength(5)]
        public string StretchPct { get; set; }

        //[StringLength(DataLengthConstant.LENGTH_KEY)]
        public string AfterSHTHrs { get; set; }

        //[StringLength(DataLengthConstant.LENGTH_KEY)]
        public string AfterSHTMns { get; set; }

        //[StringLength(DataLengthConstant.LENGTH_KEY)]
        public string NatAgingHrs { get; set; }

        public string NatAgingMns { get; set; }

        //[StringLength(DataLengthConstant.LENGTH_KEY)]
        public string ArtStartHrs { get; set; }
        public List<SelectListItem> ArtStartHours { get; set; }

        //[StringLength(DataLengthConstant.LENGTH_KEY)]
        public string ArtStartMns { get; set; }
        public List<SelectListItem> ArtStartMinutes { get; set; }

        //public DateTime? ArtAgeDate { get; set; }
        public string ArtAgeDate { get; set; }

        //[StringLength(5)]
        public string ArtAgeTemp1 { get; set; }

        //[StringLength(DataLengthConstant.LENGTH_KEY)]
        public string ArtAgeHrs1 { get; set; }

        //[StringLength(DataLengthConstant.LENGTH_KEY)]
        public string ArtAgeMns1 { get; set; }

        //[StringLength(5)]
        public string ArtAgeTemp2 { get; set; }

        //[StringLength(DataLengthConstant.LENGTH_KEY)]
        public string ArtAgeHrs2 { get; set; }


        //[StringLength(DataLengthConstant.LENGTH_KEY)]
        public string ArtAgeMns2 { get; set; }

        //[StringLength(5)]
        public string ArtAgeTemp3 { get; set; }

        //[StringLength(DataLengthConstant.LENGTH_KEY)]
        public string ArtAgeHrs3 { get; set; }


        //[StringLength(DataLengthConstant.LENGTH_KEY)]
        public string ArtAgeMns3 { get; set; }

        //[StringLength(DataLengthConstant.LENGHT_ID)]
        public string FinalTemper { get; set; }

        //[StringLength(DataLengthConstant.LENGHT_ID)]
        public string TargetCount { get; set; }

        //[StringLength(DataLengthConstant.LENGHT_ID)]
        public string ActualCount { get; set; }

        public string RCS { get; set; }

        public string RNDLotID{ get; set; }

        public int total { get; set; }

        public bool IsCopy { get; set; }

    }
}
