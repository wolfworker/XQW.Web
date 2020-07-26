using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace XQW.Model.Model
{
    public class CategoryModel
    {
        public string ACategoryID { get; set; }
        public string BCategoryID { get; set; }
        public string ACategoryName { get; set; }
        public string BCategoryName { get; set; }
    }

    public class ACategoryModel
    {
        public string ACategoryName { get; set; }

        public string ACategoryId { get; set; }

        public List<BCategoryModel> BCategoryList { get; set; }

        public List<BCategoryModelWithPro> BCategoryModelWithProList { get; set; }
    }


    public class BCategoryModel
    {
        public string BCategoryName { get; set; }

        public string BCategoryId { get; set; }
    }

    public class BCategoryModelWithPro
    {
        public string BCategoryName { get; set; }

        public string BCategoryId { get; set; }

        public List<ProductModel> ProductList { get; set; }
    }
}
