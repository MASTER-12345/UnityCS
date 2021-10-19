        Quaternion shotRotation = Quaternion.LookRotation(returnPositon.transform.position - Paota.transform.position);
        Paota.transform.rotation = Quaternion.Lerp(Paota.transform.rotation, shotRotation, 0.5f * Time.deltaTime);
        Paota.transform.localEulerAngles = new Vector3(0, Paota.transform.localEulerAngles.y, 0);
        //确定两者之间角度，如果对准了，就开炮
          //print("两者角度" + Vector3.Angle(vec,Paotaa.transform.forward));
            if (Vector3.Angle(vec, Paotaa.transform.forward) < 7)
            {
                print("开炮");
            }


