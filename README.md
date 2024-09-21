**DotinChalangeCode**

\*\*این ریپو صرفا برای کد چلنج داتین طراحی شده است

چالش کد :

API برای آپلود و ذخیره فایل اکسل

هدف:

ایجاد یک API که بتواند فایلهای اکسل با چند هزار رکورد را دریافت کرده و رکوردهای آن را در دیتابیس MSSQL ذخیره کند.

الزامات:

مدیریت تسکهای طولانی (Long Running Tasks):  
عدم انتظار کاربر:

برای پردازش فایلهای بزرگ که زمانبر هستند، API باید به گونهای پیادهسازی شود که کاربر نیازی به انتظار برای اتمام فرآیند نداشته باشد. به عبارت دیگر، درخواست باید به سرعت پاسخ دهد و پردازش فایل در پسزمینه انجام شود.  
\*\*مکانیزمهای مناسب:

پیشنهاد میشود از راهکارهایی مانند Background Services (در .NET) یا Hangfire برای اجرای تسکهای طولانی استفاده کنید.

فایل اکسل تست :  
[MOCK\_DATA.xlsx](https://github.com/javad10170/DotinChalangeCode/blob/main/Test/MOCK_DATA.xlsx)

موارد استفاده شده در پروژه :

1.  ASP.NET Core 8
2.  Entity Framework Core 8
3.  MediatR
4.  Rabbitmq
5.  .NET Aspire orchestration.
6.  EPPlus
