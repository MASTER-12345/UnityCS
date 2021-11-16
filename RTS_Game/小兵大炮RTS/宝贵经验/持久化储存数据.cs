
使用playerfabs
  PlayerPrefs.SetString("Name",mName);
   PlayerPrefs.SetInt("Age",mAge);
   PlayerPrefs.SetFloat("Grade",mGrade)
//读取数据
   mName=PlayerPrefs.GetString("Name","DefaultValue");
   mAge=PlayerPrefs.GetInt("Age",0);
   mGrade=PlayerPrefs.GetFloat("Grade",0F);
————————————————
版权声明：本文为CSDN博主「紫龙大侠」的原创文章，遵循CC 4.0 BY-SA版权协议，转载请附上原文出处链接及本声明。
原文链接：https://blog.csdn.net/alayeshi/article/details/40344967
