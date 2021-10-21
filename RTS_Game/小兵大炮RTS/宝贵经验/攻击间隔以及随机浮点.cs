//随机浮点
float random_Time = Random.Range(0.5f, 1.5f);

//攻击间隔
	// 弹幕间隔时间
    public float currentTime = 0.1f;
    // 弹幕计时
    private float invokeTime;
    // start方法中赋初值
    void Start()
    {
        invokeTime = currentTime;
    }
    
    void Update()
    {
        // 弹幕射击操作
        if (Input.GetKey(KeyCode.Z))
        {
        	// 按键按下时进行计时
            invokeTime += Time.deltaTime;
            // 间隔时间大于自定义时间才执行
            if (invokeTime -currentTime> 0)
            {
            	// 进行实例化弹幕、子弹等操作
                //Instantiate(*****);
                // 实例化一次后计时归零
                invokeTime = 0;
            }
        }
        // 同时，按键弹起时也对计时器归位
        if (Input.GetKeyUp(KeyCode.Z))
        {
            invokeTime = currentTime;
        }
    }
