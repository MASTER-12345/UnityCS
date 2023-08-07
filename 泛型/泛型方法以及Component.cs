//1.void return T
 public static T GetCmmonData<T>(int id)
    {
        if (typeof(T) == typeof(DRWeapon))
        {
            IDataTable<DRWeapon> dtWeapon = GameEntry.DataTable.GetDataTable<DRWeapon>();
            DRWeapon drWeapon = dtWeapon.GetDataRow(Convert.ToInt32(id));
            return (T)Convert.ChangeType(drWeapon, typeof(T));
        }
        else if (typeof(T) == typeof(DRHero))
        {
            IDataTable<DRHero> wp_ = GameEntry.DataTable.GetDataTable<DRHero>();
            DRHero hero = wp_.GetDataRow(Convert.ToInt32(id));
            return (T)Convert.ChangeType(hero, typeof(T));
        }
        else
        {
            return (T)Convert.ChangeType(0, typeof(T));
        }
    }

//2.Component
  public void TryCloseForm<T>() where T : Component
    {
        var pz = GameEntry.UI.GetAllLoadedUIForms();
        foreach (var i in pz)
        {
            if (i.GetComponent<T>())
            {
                int id = i.GetComponent<UIForm>().SerialId;
                GameEntry.UI.CloseUIForm(id);
                break;
            }
        }
    }
