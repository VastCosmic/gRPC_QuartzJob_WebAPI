**介绍**
C#.NET6.0
一个使用了Swagger和SwaggerUI的WebAPI,封装了Restful接口。
Restful接口调用了封装为dll文件的Quartz.NET实现的Job。
Job模拟实现对gRPC服务端发送请求并接受回复，模拟任务启停。

**解决方案在`"..\ebara\GrpcService\GrpcService.sln"`**

解决方案中已经创建了`job`相关的`dll`引用，

首先编译生成`QuartzJob`项目，

运行`GrpcService`，

再运行`WebAPI`。
