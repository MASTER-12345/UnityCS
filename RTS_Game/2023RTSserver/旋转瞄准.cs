  Vector3 vec = (enemy.transform.position - transform.position);
  Quaternion shotRotation = Quaternion.LookRotation(enemy.transform.position - transform.position);
  transform.rotation = Quaternion.Lerp(transform.rotation, shotRotation, 4f * Time.deltaTime);
   if (Vector3.Angle(vec, Paotaa.transform.forward) < 100)
                    {}
        
        
