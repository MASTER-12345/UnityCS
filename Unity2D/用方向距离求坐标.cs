float atk_h = atkJoystick.axisX.axisValue;
float atk_v = atkJoystick.axisY.axisValue;


Vector3 Joy_dir = new Vector3(atk_h, atk_v, 0);
Vector3 newpos = (transform.position + Joy_dir.normalized * 3);//normalized归一化


