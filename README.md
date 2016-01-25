# KairosApi
A .NET API for the kairos.com Facial Recognition API written in C#.


# Recognize
   KairosClient Client = new KairosClient("your_app_id", "your_app_key");
   string result = Client.recognize("http://www.link.to/your/image.jpg", "gallery1",null,null,null,"10");

# Enroll New Subject
KairosClient Client = new KairosClient("your_app_id", "your_app_key");
string result = Client.enroll("http://www.link.to/your/image.jpg", "gallery1","subject1",null,null,null);


# Other 
You can find other methods to ( list views , galleries etc.). 
