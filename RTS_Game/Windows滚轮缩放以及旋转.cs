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
         if (Input.GetKey(KeyCode.Q))
        {
            transform.rotation = Quaternion.Euler(transform.rotation.eulerAngles + new Vector3(0, 0.1f, 0));
        }
        else if (Input.GetKey(KeyCode.E))
        {
            transform.rotation = Quaternion.Euler(transform.rotation.eulerAngles - new Vector3(0, 0.1f, 0));
        }
    }
    //update里调用此函数
