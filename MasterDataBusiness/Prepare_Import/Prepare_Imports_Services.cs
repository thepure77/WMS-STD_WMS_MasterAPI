using DataAccess;
using GenAutoNumber;
using MasterBusiness;
using MasterDataAPI.Controllers;
using MasterDataBusiness.ViewModels;
using MasterDataDataAccess.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;

namespace MasterDataBusiness
{

    public  class Prepare_Imports_Services
    {
        private MasterDataDbContext db;

        public Prepare_Imports_Services()
        {
            db = new MasterDataDbContext();
        }

        public Prepare_Imports_Services(MasterDataDbContext db)
        {
            this.db = db;
        }

        #region filter
        public List<Prepare_ImportsViewModel> filter(SearchPrepare_ImportsViewModel data)
        {
     

            var query = db._Prepare_Imports.AsQueryable();
            query = query.Where(c => c.Import_Status != -1 );

            bool IsWhere = false;
            if (!string.IsNullOrEmpty(data.Import_File_Name))
            {
                query = query.Where(c => c.Import_File_Name == data.Import_File_Name );
                IsWhere = true;

            }

            if (!string.IsNullOrEmpty(data.Import_Index.ToString()))
            {
                query = query.Where(c => c.Import_Index == data.Import_Index);
                IsWhere = true;
            }

            if (!string.IsNullOrEmpty(data.Import_Type))
            {
                query = query.Where(c => c.Import_Type == data.Import_Type);
                IsWhere = true;
            }

            if ( IsWhere == false ) {
                query = query.Take(1000);
            }
           
            var list = query.ToList();

            var listdata = new List<Prepare_ImportsViewModel>();
            foreach (var item in list)
            {
                var ItemData = new Prepare_ImportsViewModel();

                ItemData.rowIndex = item.RowIndex;
                ItemData.import_Index = item.Import_Index;
                ItemData.import_Date = item.Import_Date;
                ItemData.import_Type = item.Import_Type;
                ItemData.import_Message = item.Import_Message;
                ItemData.import_File_Name = item.Import_File_Name;
                ItemData.import_Status = item.Import_Status;
                ItemData.import_By = item.Import_By;
                ItemData.import_Case = item.Import_Case;
                ItemData.IsHeader = item.IsHeader;
                ItemData.Seq = item.Seq;
                ItemData.C0 = item.C0;
                ItemData.C1 = item.C1;
                ItemData.C2 = item.C2;
                ItemData.C3 = item.C3;
                ItemData.C4 = item.C4;
                ItemData.C5 = item.C5;
                ItemData.C6 = item.C6;
                ItemData.C7 = item.C7;
                ItemData.C8 = item.C8;
                ItemData.C9 = item.C9;
                ItemData.C10 = item.C10;
                ItemData.C11 = item.C11;
                ItemData.C12 = item.C12;
                ItemData.C13 = item.C13;
                ItemData.C14 = item.C14;
                ItemData.C15 = item.C15;
                ItemData.C16 = item.C16;
                ItemData.C17 = item.C17;
                ItemData.C18 = item.C18;
                ItemData.C19 = item.C19;
                ItemData.C20 = item.C20;
                ItemData.C21 = item.C21;
                ItemData.C22 = item.C22;
                ItemData.C23 = item.C23;
                ItemData.C24 = item.C24;
                ItemData.C25 = item.C25;
                ItemData.C26 = item.C26;
                ItemData.C27 = item.C27;
                ItemData.C28 = item.C28;
                ItemData.C29 = item.C29;
                ItemData.C30 = item.C30;
                ItemData.C31 = item.C31;
                ItemData.C32 = item.C32;
                ItemData.C33 = item.C33;
                ItemData.C34 = item.C34;
                ItemData.C35 = item.C35;
                ItemData.C36 = item.C36;
                ItemData.C37 = item.C37;
                ItemData.C38 = item.C38;
                ItemData.C39 = item.C39;
                ItemData.C40 = item.C40;
                ItemData.C41 = item.C41;
                ItemData.C42 = item.C42;
                ItemData.C43 = item.C43;
                ItemData.C44 = item.C44;
                ItemData.C45 = item.C45;
                ItemData.C46 = item.C46;
                ItemData.C47 = item.C47;
                ItemData.C48 = item.C48;
                ItemData.C49 = item.C49;
                ItemData.C50 = item.C50;
                ItemData.C51 = item.C51;
                ItemData.C52 = item.C52;
                ItemData.C53 = item.C53;
                ItemData.C54 = item.C54;
                ItemData.C55 = item.C55;
                ItemData.C56 = item.C56;
                ItemData.C57 = item.C57;
                ItemData.C58 = item.C58;
                ItemData.C59 = item.C59;
                ItemData.C60 = item.C60;
                ItemData.C61 = item.C61;
                ItemData.C62 = item.C62;
                ItemData.C63 = item.C63;
                ItemData.C64 = item.C64;
                ItemData.C65 = item.C65;
                ItemData.C66 = item.C66;
                ItemData.C67 = item.C67;
                ItemData.C68 = item.C68;
                ItemData.C69 = item.C69;
                ItemData.C70 = item.C70;
                ItemData.C71 = item.C71;
                ItemData.C72 = item.C72;
                ItemData.C73 = item.C73;
                ItemData.C74 = item.C74;
                ItemData.C75 = item.C75;
                ItemData.C76 = item.C76;
                ItemData.C77 = item.C77;
                ItemData.C78 = item.C78;
                ItemData.C79 = item.C79;
                ItemData.C80 = item.C80;
                ItemData.C81 = item.C81;
                ItemData.C82 = item.C82;
                ItemData.C83 = item.C83;
                ItemData.C84 = item.C84;
                ItemData.C85 = item.C85;
                ItemData.C86 = item.C86;
                ItemData.C87 = item.C87;
                ItemData.C88 = item.C88;
                ItemData.C89 = item.C89;
                ItemData.C90 = item.C90;
                ItemData.C91 = item.C91;
                ItemData.C92 = item.C92;
                ItemData.C93 = item.C93;
                ItemData.C94 = item.C94;
                ItemData.C95 = item.C95;
                ItemData.C96 = item.C96;
                ItemData.C97 = item.C97;
                ItemData.C98 = item.C98;
                ItemData.C99 = item.C99;
                ItemData.C100 = item.C100;
                ItemData.C101 = item.C101;
                ItemData.C102 = item.C102;
                ItemData.C103 = item.C103;
                ItemData.C104 = item.C104;
                ItemData.C105 = item.C105;
                ItemData.C106 = item.C106;
                ItemData.C107 = item.C107;
                ItemData.C108 = item.C108;
                ItemData.C109 = item.C109;
                ItemData.C110 = item.C110;
                ItemData.C111 = item.C111;
                ItemData.C112 = item.C112;
                ItemData.C113 = item.C113;
                ItemData.C114 = item.C114;
                ItemData.C115 = item.C115;
                ItemData.C116 = item.C116;
                ItemData.C117 = item.C117;
                ItemData.C118 = item.C118;
                ItemData.C119 = item.C119;
                ItemData.C120 = item.C120;
                ItemData.C121 = item.C121;
                ItemData.C122 = item.C122;
                ItemData.C123 = item.C123;
                ItemData.C124 = item.C124;
                ItemData.C125 = item.C125;
                ItemData.C126 = item.C126;
                ItemData.C127 = item.C127;
                ItemData.C128 = item.C128;
                ItemData.C129 = item.C129;
                ItemData.C130 = item.C130;
                ItemData.C131 = item.C131;
                ItemData.C132 = item.C132;
                ItemData.C133 = item.C133;
                ItemData.C134 = item.C134;
                ItemData.C135 = item.C135;
                ItemData.C136 = item.C136;
                ItemData.C137 = item.C137;
                ItemData.C138 = item.C138;
                ItemData.C139 = item.C139;
                ItemData.C140 = item.C140;
                ItemData.C141 = item.C141;
                ItemData.C142 = item.C142;
                ItemData.C143 = item.C143;
                ItemData.C144 = item.C144;
                ItemData.C145 = item.C145;
                ItemData.C146 = item.C146;
                ItemData.C147 = item.C147;
                ItemData.C148 = item.C148;
                ItemData.C149 = item.C149;
                ItemData.C150 = item.C150;
                ItemData.C151 = item.C151;
                ItemData.C152 = item.C152;
                ItemData.C153 = item.C153;
                ItemData.C154 = item.C154;
                ItemData.C155 = item.C155;
                ItemData.C156 = item.C156;
                ItemData.C157 = item.C157;
                ItemData.C158 = item.C158;
                ItemData.C159 = item.C159;
                ItemData.C160 = item.C160;
                ItemData.C161 = item.C161;
                ItemData.C162 = item.C162;
                ItemData.C163 = item.C163;
                ItemData.C164 = item.C164;
                ItemData.C165 = item.C165;
                ItemData.C166 = item.C166;
                ItemData.C167 = item.C167;
                ItemData.C168 = item.C168;
                ItemData.C169 = item.C169;
                ItemData.C170 = item.C170;
                ItemData.C171 = item.C171;
                ItemData.C172 = item.C172;
                ItemData.C173 = item.C173;
                ItemData.C174 = item.C174;
                ItemData.C175 = item.C175;
                ItemData.C176 = item.C176;
                ItemData.C177 = item.C177;
                ItemData.C178 = item.C178;
                ItemData.C179 = item.C179;
                ItemData.C180 = item.C180;
                ItemData.C181 = item.C181;
                ItemData.C182 = item.C182;
                ItemData.C183 = item.C183;
                ItemData.C184 = item.C184;
                ItemData.C185 = item.C185;
                ItemData.C186 = item.C186;
                ItemData.C187 = item.C187;
                ItemData.C188 = item.C188;
                ItemData.C189 = item.C189;
                ItemData.C190 = item.C190;
                ItemData.C191 = item.C191;
                ItemData.C192 = item.C192;
                ItemData.C193 = item.C193;
                ItemData.C194 = item.C194;
                ItemData.C195 = item.C195;
                ItemData.C196 = item.C196;
                ItemData.C197 = item.C197;
                ItemData.C198 = item.C198;
                ItemData.C199 = item.C199;
                ItemData.C200 = item.C200;


                listdata.Add(ItemData);


            }


                return listdata;
        }


        #endregion

        #region Create
        public String Insert_Prepare_Imports(List<Prepare_ImportsViewModel> data)
        {

            var listPrepare_Imports = new List<_Prepare_Imports>();

            foreach (var item in data)
            {
                _Prepare_Imports ItemData = new _Prepare_Imports();

                ItemData.RowIndex = item.rowIndex;
                ItemData.Import_Index = item.import_Index;
                ItemData.Import_Date = item.import_Date;
                ItemData.Import_Type = item.import_Type;
                ItemData.Import_Message = item.import_Message;
                ItemData.Import_File_Name = item.import_File_Name;
                ItemData.Import_Status = item.import_Status;
                ItemData.Import_By = item.import_By;
                ItemData.Import_Case = item.import_Case;
                ItemData.IsHeader = item.IsHeader;
                ItemData.Seq = item.Seq;
                ItemData.C0 = item.C0;
                ItemData.C1 = item.C1;
                ItemData.C2 = item.C2;
                ItemData.C3 = item.C3;
                ItemData.C4 = item.C4;
                ItemData.C5 = item.C5;
                ItemData.C6 = item.C6;
                ItemData.C7 = item.C7;
                ItemData.C8 = item.C8;
                ItemData.C9 = item.C9;
                ItemData.C10 = item.C10;
                ItemData.C11 = item.C11;
                ItemData.C12 = item.C12;
                ItemData.C13 = item.C13;
                ItemData.C14 = item.C14;
                ItemData.C15 = item.C15;
                ItemData.C16 = item.C16;
                ItemData.C17 = item.C17;
                ItemData.C18 = item.C18;
                ItemData.C19 = item.C19;
                ItemData.C20 = item.C20;
                ItemData.C21 = item.C21;
                ItemData.C22 = item.C22;
                ItemData.C23 = item.C23;
                ItemData.C24 = item.C24;
                ItemData.C25 = item.C25;
                ItemData.C26 = item.C26;
                ItemData.C27 = item.C27;
                ItemData.C28 = item.C28;
                ItemData.C29 = item.C29;
                ItemData.C30 = item.C30;
                ItemData.C31 = item.C31;
                ItemData.C32 = item.C32;
                ItemData.C33 = item.C33;
                ItemData.C34 = item.C34;
                ItemData.C35 = item.C35;
                ItemData.C36 = item.C36;
                ItemData.C37 = item.C37;
                ItemData.C38 = item.C38;
                ItemData.C39 = item.C39;
                ItemData.C40 = item.C40;
                ItemData.C41 = item.C41;
                ItemData.C42 = item.C42;
                ItemData.C43 = item.C43;
                ItemData.C44 = item.C44;
                ItemData.C45 = item.C45;
                ItemData.C46 = item.C46;
                ItemData.C47 = item.C47;
                ItemData.C48 = item.C48;
                ItemData.C49 = item.C49;
                ItemData.C50 = item.C50;
                ItemData.C51 = item.C51;
                ItemData.C52 = item.C52;
                ItemData.C53 = item.C53;
                ItemData.C54 = item.C54;
                ItemData.C55 = item.C55;
                ItemData.C56 = item.C56;
                ItemData.C57 = item.C57;
                ItemData.C58 = item.C58;
                ItemData.C59 = item.C59;
                ItemData.C60 = item.C60;
                ItemData.C61 = item.C61;
                ItemData.C62 = item.C62;
                ItemData.C63 = item.C63;
                ItemData.C64 = item.C64;
                ItemData.C65 = item.C65;
                ItemData.C66 = item.C66;
                ItemData.C67 = item.C67;
                ItemData.C68 = item.C68;
                ItemData.C69 = item.C69;
                ItemData.C70 = item.C70;
                ItemData.C71 = item.C71;
                ItemData.C72 = item.C72;
                ItemData.C73 = item.C73;
                ItemData.C74 = item.C74;
                ItemData.C75 = item.C75;
                ItemData.C76 = item.C76;
                ItemData.C77 = item.C77;
                ItemData.C78 = item.C78;
                ItemData.C79 = item.C79;
                ItemData.C80 = item.C80;
                ItemData.C81 = item.C81;
                ItemData.C82 = item.C82;
                ItemData.C83 = item.C83;
                ItemData.C84 = item.C84;
                ItemData.C85 = item.C85;
                ItemData.C86 = item.C86;
                ItemData.C87 = item.C87;
                ItemData.C88 = item.C88;
                ItemData.C89 = item.C89;
                ItemData.C90 = item.C90;
                ItemData.C91 = item.C91;
                ItemData.C92 = item.C92;
                ItemData.C93 = item.C93;
                ItemData.C94 = item.C94;
                ItemData.C95 = item.C95;
                ItemData.C96 = item.C96;
                ItemData.C97 = item.C97;
                ItemData.C98 = item.C98;
                ItemData.C99 = item.C99;
                ItemData.C100 = item.C100;
                ItemData.C101 = item.C101;
                ItemData.C102 = item.C102;
                ItemData.C103 = item.C103;
                ItemData.C104 = item.C104;
                ItemData.C105 = item.C105;
                ItemData.C106 = item.C106;
                ItemData.C107 = item.C107;
                ItemData.C108 = item.C108;
                ItemData.C109 = item.C109;
                ItemData.C110 = item.C110;
                ItemData.C111 = item.C111;
                ItemData.C112 = item.C112;
                ItemData.C113 = item.C113;
                ItemData.C114 = item.C114;
                ItemData.C115 = item.C115;
                ItemData.C116 = item.C116;
                ItemData.C117 = item.C117;
                ItemData.C118 = item.C118;
                ItemData.C119 = item.C119;
                ItemData.C120 = item.C120;
                ItemData.C121 = item.C121;
                ItemData.C122 = item.C122;
                ItemData.C123 = item.C123;
                ItemData.C124 = item.C124;
                ItemData.C125 = item.C125;
                ItemData.C126 = item.C126;
                ItemData.C127 = item.C127;
                ItemData.C128 = item.C128;
                ItemData.C129 = item.C129;
                ItemData.C130 = item.C130;
                ItemData.C131 = item.C131;
                ItemData.C132 = item.C132;
                ItemData.C133 = item.C133;
                ItemData.C134 = item.C134;
                ItemData.C135 = item.C135;
                ItemData.C136 = item.C136;
                ItemData.C137 = item.C137;
                ItemData.C138 = item.C138;
                ItemData.C139 = item.C139;
                ItemData.C140 = item.C140;
                ItemData.C141 = item.C141;
                ItemData.C142 = item.C142;
                ItemData.C143 = item.C143;
                ItemData.C144 = item.C144;
                ItemData.C145 = item.C145;
                ItemData.C146 = item.C146;
                ItemData.C147 = item.C147;
                ItemData.C148 = item.C148;
                ItemData.C149 = item.C149;
                ItemData.C150 = item.C150;
                ItemData.C151 = item.C151;
                ItemData.C152 = item.C152;
                ItemData.C153 = item.C153;
                ItemData.C154 = item.C154;
                ItemData.C155 = item.C155;
                ItemData.C156 = item.C156;
                ItemData.C157 = item.C157;
                ItemData.C158 = item.C158;
                ItemData.C159 = item.C159;
                ItemData.C160 = item.C160;
                ItemData.C161 = item.C161;
                ItemData.C162 = item.C162;
                ItemData.C163 = item.C163;
                ItemData.C164 = item.C164;
                ItemData.C165 = item.C165;
                ItemData.C166 = item.C166;
                ItemData.C167 = item.C167;
                ItemData.C168 = item.C168;
                ItemData.C169 = item.C169;
                ItemData.C170 = item.C170;
                ItemData.C171 = item.C171;
                ItemData.C172 = item.C172;
                ItemData.C173 = item.C173;
                ItemData.C174 = item.C174;
                ItemData.C175 = item.C175;
                ItemData.C176 = item.C176;
                ItemData.C177 = item.C177;
                ItemData.C178 = item.C178;
                ItemData.C179 = item.C179;
                ItemData.C180 = item.C180;
                ItemData.C181 = item.C181;
                ItemData.C182 = item.C182;
                ItemData.C183 = item.C183;
                ItemData.C184 = item.C184;
                ItemData.C185 = item.C185;
                ItemData.C186 = item.C186;
                ItemData.C187 = item.C187;
                ItemData.C188 = item.C188;
                ItemData.C189 = item.C189;
                ItemData.C190 = item.C190;
                ItemData.C191 = item.C191;
                ItemData.C192 = item.C192;
                ItemData.C193 = item.C193;
                ItemData.C194 = item.C194;
                ItemData.C195 = item.C195;
                ItemData.C196 = item.C196;
                ItemData.C197 = item.C197;
                ItemData.C198 = item.C198;
                ItemData.C199 = item.C199;
                ItemData.C200 = item.C200;


              db._Prepare_Imports.Add(ItemData);

            }

            var result = db.SaveChanges();

            return "";

        }
        #endregion


    }
}
