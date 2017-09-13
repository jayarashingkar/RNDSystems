using System;


namespace RNDSystems.Models
{
    /// <summary>
    /// Testing column details
    /// </summary>
    public class RNDTesting : IRNDModel
    {
        public int TestingNo { get; set; }

        //[StringLength(DataLengthConstant.LENGHT_ID)]
        public string WorkStudyID { get; set; }

        //[StringLength(DataLengthConstant.LENGHT_ID)]
        public string LotID { get; set; }

        public int MillLotNo { get; set; }

        //[StringLength(DataLengthConstant.LENGHT_ID)]
        public string SoNum { get; set; }

        //[StringLength(DataLengthConstant.LENGTH_KEY)]
        public string Hole { get; set; }

        //[StringLength(DataLengthConstant.LENGTH_KEY)]
        public string PieceNo { get; set; }

        //[StringLength(DataLengthConstant.LENGHT_ID)]
        public string Alloy { get; set; }

        //[StringLength(DataLengthConstant.LENGHT_ID)]
        public string Temper { get; set; }

        //[StringLength(30)]
        public string CustPart { get; set; }

        //check numeric(6,0)
        public decimal UACPart { get; set; }

        //[StringLength(7)]
        public string GageThickness { get; set; }

        //[StringLength(4)]
        public string Orientation { get; set; }

        //[StringLength(10)]
        public string Location1 { get; set; }

        //[StringLength(6)]
        public string Location2 { get; set; }

        //[StringLength(6)]
        public string Location3 { get; set; }

        //[StringLength(60)]
        public string SpeciComment { get; set; }

        //[StringLength(35)]
        public string TestType { get; set; }

        //[StringLength(35)]
        public string SubTestType { get; set; }

        public char Status { get; set; }
        public char Selected { get; set; }

        public char Printed { get; set; }

        //[StringLength(2)]
        public string Replica { get; set; }

        public char RCS { get; set; }

        #region IRNDModel

        //[StringLength(25)]
        public string EntryBy { get; set; }

        public DateTime? EntryDate { get; set; }

        #endregion

        public int total { get; set; }

    }

}

