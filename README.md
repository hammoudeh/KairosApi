# KairosApi
A .NET API for the kairos.com Facial Recognition API written in C#.


#Recognize
   KairosClient c = new KairosClient("your_app_id", "your_app_key");
   string s = c.recognize("http://www.link.to/your/image.jpg", "gallery1",null,null,null,"10");

