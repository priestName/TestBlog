using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using BlogModels;
using TestBlog.IService;
using Microsoft.AspNetCore.Cors;
using System.Linq.Expressions;
using System;
using TestBlog.Models;

namespace TestBlog.Controllers
{
    [EnableCors("any")]
    [Route("api/[controller]")]
    [ApiController]
    public class ContentListsController : ControllerBase
    {
        public IContentListService ContentListService { get; set; }
        public PageModel Pages = new PageModel();
        //api/ContentLists/Page/1
        [HttpGet("Page/{PageIndex}")]
        public async Task<IEnumerable<ContentList>> GetContentList(int PageIndex = 1, int PageSize = 20)//ContentList contentList, KeyValueModel keyValue,
        {
            ContentList contentList = new ContentList() { Content = "Test",Author = "Test"}; KeyValueModel keyValue = new KeyValueModel() { };
            var abcdefg = WhereLambdas(contentList, keyValue);
            int a1 = ContentListService.GetCount(abcdefg).Result;
            Expression<Func<ContentList, bool>> exp = StringToLambda.LambdaParser.Parse<ContentList>("s=>s.Content.Contains(\"Test\")&&s.Author == \"Test\"");
            int a2 = ContentListService.GetCount(exp).Result;
            Expression<Func<ContentList, bool>> exp0 = StringToLambda.LambdaParser.Parse<ContentList>("s=>s.Author == \"Test\"");
            Expression <Func<ContentList, bool>> exp1 = StringToLambda.LambdaParser.Parse<ContentList>("s=>s.Content.Contains(\"Test\")");
            Expression<Func<ContentList, bool>> exp2 = StringToLambda.LambdaParser.Parse<ContentList>("s => s.Time");
            Pages.PageSize = PageSize; Pages.PageIndex = PageIndex;
            return await ContentListService.GetEntitiesByPpage(Pages.PageSize, Pages.PageIndex, false, s=>s.Content.Contains("Test")&&s.Author == "Test", exp2);//WhereLambdas(contentList, keyValue)
        }
        //api/ContentLists/Count
        [HttpGet("Count")]
        public PageModel GetContentCount()//ContentList contentList, KeyValueModel keyValue
        {
            Expression<Func<ContentList, bool>> exp = StringToLambda.LambdaParser.Parse<ContentList>("s=>s.Content.Contains('Test')&&s.Author == 'Test'");
            Pages.TableCount = ContentListService.GetCount(exp).Result;//WhereLambdas(contentList, keyValue)
            Pages.PageCount = Pages.TableCount / Pages.PageSize;
            return Pages;
        }
        //Blog/ContentLists/Content/1
        [HttpGet("Content/{id}")]
        public async Task<ActionResult<ContentList>> GetContentList(int id)
        {
            var contentList = await ContentListService.GetEntity(s => s.Id == id);

            if (contentList == null)
            {
                return NotFound();
            }

            return contentList;
        }
        //Blog/ContentLists/Update/1
        [HttpPut("Update/{id}")]
        public async Task<IActionResult> PutContentList(int id, ContentList contentList)
        {
            if (id != contentList.Id)
            {
                return BadRequest();
            }
            try
            {
                await Task.Run(() =>
                {
                    ContentListService.Modify(contentList);
                });
                //await ContentListService.Modify(contentList);
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ContentListExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }
        //Blog/ContentLists/Add
        [HttpPost("Add")]
        public async Task<ActionResult<ContentList>> PostContentList(ContentList contentList)
        {
            await Task.Run(() =>
            {
                ContentListService.Add(contentList);
            });

            return CreatedAtAction("GetContentList", new { id = contentList.Id }, contentList);
        }
        //Blog/ContentLists/Delete/1
        [HttpDelete("Delete/{id}")]
        public async Task<ActionResult<ContentList>> DeleteContentList(int id)
        {

            ContentList contentList = ContentListService.GetEntity(s => s.Id == id).Result;
            if (contentList == null)
            {
                return NotFound();
            }
            await Task.Run(() =>
            {
                ContentListService.Remov(contentList);
            });

            return contentList;
        }
        private bool ContentListExists(int id)
        {
            return ContentListService.GetCount(e => e.Id == id).Result > 0;
        }
        private Expression<Func<ContentList, bool>> WhereLambdas(ContentList contentList,KeyValueModel keyValue)
        {
            Expression<Func<ContentList, bool>> where = s=>s.IsDraft == false;
            if (contentList != null)
            {
                if (contentList.Id != 0)
                    where = Helper.LambdasHelper.And(where, s => s.Id == contentList.Id);
                if (string.IsNullOrEmpty(contentList.Title))
                    where = Helper.LambdasHelper.And(where, s => s.Title == contentList.Title);
                if (string.IsNullOrEmpty(contentList.Content))
                    where = Helper.LambdasHelper.And(where, s => s.Content.Contains(contentList.Content));
                if (contentList.IsShow != null)
                    where = Helper.LambdasHelper.And(where, s => s.IsShow == contentList.IsShow);
                if (string.IsNullOrEmpty(contentList.Label))
                    where = Helper.LambdasHelper.And(where, s => s.Label.Contains(contentList.Label));
                if (string.IsNullOrEmpty(contentList.Author))
                    where = Helper.LambdasHelper.And(where, s => s.Author.Contains(contentList.Author));
                if (string.IsNullOrEmpty(contentList.TypeValue))
                    where = Helper.LambdasHelper.And(where, s => s.TypeValue.Contains(contentList.TypeValue));
            }
            if (keyValue != null)
            {
                if (keyValue.StartDate != null)
                    Helper.LambdasHelper.And(where, s => s.Time.CompareTo(TextToDateTime(keyValue.StartDate)) >= 0);
                if (keyValue.EndDate != null)
                    Helper.LambdasHelper.And(where, s => s.Time.CompareTo(TextToDateTime(keyValue.EndDate)) <= 0);
                if (keyValue.StartDate != null)
                    Helper.LambdasHelper.And(where, s => TextToDateTime(keyValue.StartDate2).CompareTo((DateTime)s.LastTime) >= 0);
                if (keyValue.EndDate != null)
                    Helper.LambdasHelper.And(where, s => TextToDateTime(keyValue.EndDate2).CompareTo((DateTime)s.LastTime) <= 0);
            }
            return where;
        }
        public static DateTime TextToDateTime(string text)
        {
            DateTime result = DateTime.Parse(DateTime.Now.ToString("yyyy-MM-dd"));

            DateTime.TryParse(text, out result);
            if (result == DateTime.MinValue)
                return DateTime.Parse(DateTime.Now.ToString("yyyy-MM-dd"));
            else
                return result;
        }
    }
}
