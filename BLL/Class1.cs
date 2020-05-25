using System;
using System.Data;
using System.Collections.Generic;
using Model;
using DAL;
namespace BLL
{
    //t_attendanceClockGoGo
    public partial class t_attendanceClockGoGo
    {
        private readonly DAL.t_attendanceClockGoGo dal = new DAL.t_attendanceClockGoGo();
        public t_attendanceClockGoGo()
        { }

        #region  Method
        /// <summary>
        /// 是否存在该记录
        /// </summary>
        public bool Exists(int autoid)
        {
            return dal.Exists(autoid);
        }

        /// <summary>
        /// 增加一条数据
        /// </summary>
        public int Add(Model.t_attendanceClockGoGo model)
        {
            return dal.Add(model);
        }

        /// <summary>
        /// 更新一条数据
        /// </summary>
        public bool Update(Model.t_attendanceClockGoGo model)
        {
            return dal.Update(model);
        }

        /// <summary>
        /// 删除一条数据
        /// </summary>
        public bool Delete(int autoid)
        {
            return dal.Delete(autoid);
        }

        /// <summary>
        /// 批量删除一批数据
        /// </summary>
        public bool DeleteList(string autoidlist)
        {
            return dal.DeleteList(autoidlist);
        }

        /// <summary>
        /// 得到一个对象实体
        /// </summary>
        public Model.t_attendanceClockGoGo GetModel(int autoid)
        {
            return dal.GetModel(autoid);
        }

        /// <summary>
        /// 获得数据列表
        /// </summary>
        public DataTable GetList(string strWhere)
        {
            return dal.GetList(strWhere);
        }
        /// <summary>
        /// 获得前几行数据
        /// </summary>
        public DataTable GetList(int Top, string strWhere, string filedOrder)
        {
            return dal.GetList(Top, strWhere, filedOrder);
        }
        /// <summary>
        /// 获得数据列表
        /// </summary>
        public List<Model.t_attendanceClockGoGo> GetModelList(string strWhere)
        {
            DataTable dt = dal.GetList(strWhere);
            return DataTableToList(dt);
        }
        /// <summary>
        /// 获得数据列表
        /// </summary>
        public List<Model.t_attendanceClockGoGo> DataTableToList(DataTable dt)
        {
            List<Model.t_attendanceClockGoGo> modelList = new List<Model.t_attendanceClockGoGo>();
            int rowsCount = dt.Rows.Count;
            if (rowsCount > 0)
            {
                Model.t_attendanceClockGoGo model;
                for (int n = 0; n < rowsCount; n++)
                {
                    model = new Model.t_attendanceClockGoGo();
                    if (dt.Rows[n]["autoid"].ToString() != "")
                    {
                        model.autoid = int.Parse(dt.Rows[n]["autoid"].ToString());
                    }
                    if (dt.Rows[n]["isBiometric"].ToString() != "")
                    {
                        if ((dt.Rows[n]["isBiometric"].ToString() == "1") || (dt.Rows[n]["isBiometric"].ToString().ToLower() == "true"))
                        {
                            model.isBiometric = true;
                        }
                        else
                        {
                            model.isBiometric = false;
                        }
                    }
                    if (dt.Rows[n]["id"].ToString() != "")
                    {
                        model.id = long.Parse(dt.Rows[n]["id"].ToString());
                    }
                    model.time = dt.Rows[n]["time"].ToString();
                    if (dt.Rows[n]["createTime"].ToString() != "")
                    {
                        model.createTime = DateTime.Parse(dt.Rows[n]["createTime"].ToString());
                    }
                    model.date = dt.Rows[n]["date"].ToString();
                    model.workspotName = dt.Rows[n]["workspotName"].ToString();
                    model.employmentCode = dt.Rows[n]["employmentCode"].ToString();
                    if (dt.Rows[n]["gpsLat"].ToString() != "")
                    {
                        model.gpsLat = decimal.Parse(dt.Rows[n]["gpsLat"].ToString());
                    }
                    model.cardType = dt.Rows[n]["cardType"].ToString();
                    model.workspotCode = dt.Rows[n]["workspotCode"].ToString();
                    if (dt.Rows[n]["gpsLng"].ToString() != "")
                    {
                        model.gpsLng = decimal.Parse(dt.Rows[n]["gpsLng"].ToString());
                    }


                    modelList.Add(model);
                }
            }
            return modelList;
        }

        /// <summary>
        /// 获得数据列表
        /// </summary>
        public DataTable GetAllList()
        {
            return GetList("");
        }

        /// <summary>
        /// GetPage("gs_id>3", "gs_id", "order by gs_id", 2, 3, out recordCount, out pageCount);
        /// </summary>
        /// <param name="Where">"" or "gs_id>3"</param>
        /// <param name="IndexField">"gs_id"</param>
        /// <param name="OrderFields">"order by gs_id" or "order by gs_id desc"</param>
        /// <param name="PageIndex">2</param>
        /// <param name="PageSize">3</param>
        /// <param name="RecordCount">out recordCount</param>
        /// <param name="PageCount">out pageCount</param>
        /// <returns></returns>
        public DataTable GetPage(string Where, string IndexField, string OrderFields, int PageIndex, int PageSize, out int RecordCount, out int PageCount)
        {
            return dal.GetPage(Where, IndexField, OrderFields, PageIndex, PageSize, out RecordCount, out PageCount);
        }
        #endregion

    }
}