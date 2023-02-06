using House.Dto;
using House.IRepository.Contract;
using House.IRepository.ICustomerManagement;
using House.Model.CustomerManagement;
using House.Repository.Customer;
using LinqKit;
using MathNet.Numerics.RootFinding;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using NPOI.HSSF.UserModel;
using NPOI.OpenXmlFormats.Wordprocessing;
using NPOI.XWPF.UserModel;
using System.Configuration;
using System.IO;
using System.Threading.Tasks;
using static System.Net.Mime.MediaTypeNames;

namespace House.API.Controllers.Customer
{
    /// <summary>
    /// 合同控制器
    /// </summary>
    [Route("api/[controller]/[action]")]
    [ApiController]
    [ApiExplorerSettings(GroupName = "Customer")]
    public class CustomerinfoController : ControllerBase
    {
        private readonly ICustomerinfoRepository _ICustomerinfoRepository;

        private readonly IContractChargesRepository _IContractChargesRepository;

        private readonly IContractInfoRepository _IContractInfoRepository;

        private readonly ISubscriptioninfoRepository _ISubscriptioninfoRepository;

        public CustomerinfoController(ICustomerinfoRepository icustomerinforepository, IContractChargesRepository IContractChargesRepository, IContractInfoRepository IContractInfoRepository, ISubscriptioninfoRepository ISubscriptioninfoRepository)
        {
            _ICustomerinfoRepository = icustomerinforepository;
            _IContractChargesRepository = IContractChargesRepository;
            _IContractInfoRepository = IContractInfoRepository;
            _ISubscriptioninfoRepository = ISubscriptioninfoRepository;
        }

        /// <summary>
        /// 客户信息 (用于合同录入时选择)
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public async Task<PageModel<Customerinfo>> Customers()
        {
            var data = await _ICustomerinfoRepository.GetAllListAsync();
            return new PageModel<Customerinfo> { Data = data };
        }

        /// <summary>
        /// 根据选中客户获取详情
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public async Task<PageModel<Customerinfo>> GetByNumber(string number)
        {
            var predicate = PredicateBuilder.New<Customerinfo>();
            predicate.And(t => t.Number == number);
            var data = await _ICustomerinfoRepository.FirstOrDefaultAsync(predicate);
            return new PageModel<Customerinfo> { Item = data };
        }

        /// <summary>
        /// 合同添加
        /// </summary>
        /// <param name="customedto"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<bool> CustomeAdd(CustomeDto customedto)
        {
            try
            {
                await _IContractInfoRepository.InsertAsync(customedto.contractinfo);

                foreach (var item in customedto.subscriptioninfo)
                {
                    await _ISubscriptioninfoRepository.InsertAsync(item);
                }
                return true;
            }
            catch (System.Exception)
            {
                return false;
                throw;
            }
        }

        /// <summary>
        /// 导出数据到word中
        /// </summary>
        [HttpGet]
        public async Task<FileResult> CustomWordDownload()
        {
            //1、初始化文档
            XWPFDocument m_Doc = new XWPFDocument();
            CT_SectPr m_SectPrs = new CT_SectPr();                      //设置页面格式(宽度)        A4横向
            m_SectPrs.pgSz.w = (ulong)16838;
            m_SectPrs.pgSz.h = (ulong)11906;
            m_Doc.Document.body.sectPr = m_SectPrs;

            //2、创建主标题段落
            XWPFParagraph p1 = m_Doc.CreateParagraph();
            p1.Alignment = ParagraphAlignment.LEFT;
            XWPFRun row1 = p1.CreateRun();
            row1.FontFamily = "黑体";
            row1.FontSize = 14;
            row1.IsBold = true;
            row1.SetText("测试");                   //主标题
            CT_P doc_p1 = m_Doc.Document.body.GetPArray(0);
            doc_p1.AddNewPPr().AddNewJc().val = ST_Jc.center;             //段落水平居中  标题

            //3、自定义内容
            XWPFParagraph p2 = m_Doc.CreateParagraph();
            p2.Alignment = ParagraphAlignment.LEFT;
            XWPFRun row21 = p2.CreateRun();
            row21.SetText("我进行开始测试\n\n");
            XWPFRun row22 = p2.CreateRun();
            row22.SetText("\n\n我结束测试");

            //4、创建页脚
            m_Doc.Document.body.sectPr = new CT_SectPr();
            CT_SectPr m_SectPr = m_Doc.Document.body.sectPr;

            CT_Ftr m_ftr = new CT_Ftr();
            //m_ftr.AddNewP().AddNewR().AddNewT().Value = DateTime.Now.ToString("yyyy-MM-dd");
            m_ftr.AddNewP().AddNewR().AddNewT().Value = "1";
            XWPFRelation frelation = XWPFRelation.FOOTER;
            XWPFFooter m_f = (XWPFFooter)m_Doc.CreateRelationship(frelation, XWPFFactory.GetInstance(), m_Doc.FooterList.Count + 1);
            m_f.SetHeaderFooter(m_ftr);
            m_f._getHdrFtr().AddNewP().AddNewPPr().AddNewJc().val = ST_Jc.center;
            CT_HdrFtrRef m_HdrFtr = m_SectPr.AddNewFooterReference();
            m_HdrFtr.type = ST_HdrFtr.@default;
            m_HdrFtr.id = m_f.GetPackageRelationship().Id;

            //使得页脚居中显示
            XWPFParagraph pFooter = m_f.Paragraphs[0];
            pFooter.Alignment = ParagraphAlignment.CENTER; //居中
            pFooter.Runs[0].FontSize = 12;

            var fs = new MemoryStream();
            m_Doc.Write(fs);

            byte[] b = fs.ToArray();
            return File(b, System.Net.Mime.MediaTypeNames.Application.Octet, "合同信息.docx"); //关键语句
        }
    }
}