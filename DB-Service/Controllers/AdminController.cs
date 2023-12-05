// using DB_Service.Dtos;
// using DB_Service.Exceptions;
// using DB_Service.Services.Comment.CRUD;
// using DB_Service.Services.Priority.CRUD;
// using DB_Service.Services.Process.CRUD;
// using DB_Service.Services.Type.CRUD;
// using Microsoft.AspNetCore.Http;
// using Microsoft.AspNetCore.Mvc;

// namespace DB_Service.Controllers
// {
//     [Route("[controller]")]
//     [ApiController]
//     public class AdminController : ControllerBase //TODO:Initiolizer в контроллеры
//     {
//         //private readonly ICommentService _commentService;
//         private readonly IPriorityService _priorityService;
//         private readonly ITypeService _typeService;
//         private readonly IProcessService _processService;

//         public AdminController(
//             IPriorityService priorityService,
//             ITypeService typeService,
//             IProcessService processService
//             )
//         {
//             _priorityService = priorityService;
//             _typeService = typeService;
//             _processService = processService;
//         }

//         [HttpPost("process/create")]
//         public async Task<ActionResult<int>> CreateProcess(CreateProcessDto data)
//         {
//             try
//             {
//                 var res = await _processService.Create(
//                     title: data.Process.Title,
//                     description: data.Process.Description,
//                     isTemplate: true,
//                     priorityId: (int) data.Process.PriorityValue,
//                     typeId: data.TemplateId,
//                     head: null,
//                     tail: null,
//                     expectedTime: data.Process.ExpectedTime
//                     );
//                 return Ok(res);
//             }
//             catch (NotFoundException ex)
//             {
//                 return NotFound(ex.Message);
//             }
//         }

//         [HttpPost("priority/create")]
//         public async Task<ActionResult<int>> CreatePriority(string data)
//         {
//             try
//             {
//                 var res = await _priorityService.Create(title: data);
//                 return Ok(res);
//             }
//             catch (NotFoundException ex)
//             {
//                 return NotFound(ex.Message);
//             }
//         }

//         [HttpPost("type/create")]
//         public async Task<ActionResult<int>> CreateType(string data)
//         {
//             try
//             {
//                 var res = await _typeService.Create(title: data);
//                 return Ok(res);
//             }
//             catch (NotFoundException ex)
//             {
//                 return NotFound(ex.Message);
//             }
//         }

//         //[HttpGet("{Id}")]
//         //public async Task<ActionResult<CommentDto>> Get(int Id)
//         //{
//         //    try
//         //    {
//         //        var res = await _commentService.Get(Id);
//         //        return Ok(res);
//         //    }
//         //    catch (NotFoundException ex)
//         //    {
//         //        return NotFound(ex.Message);
//         //    }
//         //}

//         //[HttpGet("{Id}/Exist")]
//         //public async Task<ActionResult<Models.Comment>> Exist(int Id)
//         //{
//         //    try
//         //    {
//         //        var res = await _commentService.Exist(Id);
//         //        return Ok(res);
//         //    }
//         //    catch (NotFoundException ex)
//         //    {
//         //        return NotFound(ex.Message);
//         //    }
//         //}

//         //[HttpPost("Create")]
//         //public async Task<ActionResult<CommentDto>> Create(int taskId, CommentDto data)
//         //{
//         //    try
//         //    {
//         //        var res = await _commentService.Create(taskId, data.User.Id, data.Text, data.FileRef);
//         //        return Ok(res);
//         //    }
//         //    catch (NotFoundException ex)
//         //    {
//         //        return NotFound(ex.Message);
//         //    }
//         //}

//         //[HttpPut("Update")]
//         //public async Task<ActionResult<int>> Update(CommentDto data)
//         //{
//         //    try
//         //    {
//         //        var res = await _commentService.Update((int)data.Id, data.Text, data.FileRef);
//         //        return Ok(res);
//         //    }
//         //    catch (NotFoundException ex)
//         //    {
//         //        return NotFound(ex.Message);
//         //    }
//         //}

//         //[HttpDelete("{Id}/Delete")]
//         //public async Task<ActionResult<int>> Delete(int Id)
//         //{
//         //    try
//         //    {
//         //        var res = await _commentService.Delete(Id);
//         //        return Ok(res);
//         //    }
//         //    catch (NotFoundException ex)
//         //    {
//         //        return NotFound(ex.Message);
//         //    }
//         //}
//     }
// }
