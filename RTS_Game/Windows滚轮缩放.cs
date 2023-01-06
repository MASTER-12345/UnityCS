void Zoom()
    {
        if (Input.GetAxis("Mouse ScrollWheel") > 0)
        {

            transform.position = transform.position - new Vector3(0, scrollspeed, 0);

        }
        if (Input.GetAxis("Mouse ScrollWheel") < 0)
        {
            transform.position = transform.position + new Vector3(0, scrollspeed, 0);

        }
    }
    //update里调用此函数
