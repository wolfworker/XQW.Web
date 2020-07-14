﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace XQW.Model.Model
{
    public class ProductAllModel
    {
        public int ACateCount { get; set; }

        public int BCateCount { get; set; }

        public int ProductCount { get; set; }

        public List<ACategoryModel> ACategoryList { get; set; }
    }

    public class ACategoryModel
    {
        public string ACategoryName { get; set; }

        public string ACategoryId { get; set; }

        public List<BCategoryModel> BCategoryList { get; set; }
    }

    public class BCategoryModel
    {
        public string BCategoryName { get; set; }

        public string BCategoryId { get; set; }

        public List<ProductModel> ProductList { get; set; }
    }


    public class ProductAllOriginalModel
    {
        public string ProductName { get; set; }

        public string ProductID { get; set; }

        public string ProductTitle { get; set; }

        public string ACategoryID { get; set; }

        public string ACategoryName { get; set; }

        public string BCategoryID { get; set; }

        public string BCategoryName { get; set; }
    }
}
