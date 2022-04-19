using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace RESTuserAPI.Models
{
    public class User
    {
        private DateTime _dob;
        private string _nric;
        private static string _dobInString;

        public int UserID { get; set; }

        public string Name { get; set; }

        public string NRIC
        {
            get { return _nric; }
            set
            {
                _nric = value; ConvertDOB(value);
            }
        }

        public string DOB
        {
            get { return _dob.ToString("dd/MM/yyyy"); }
            set { ConvertDOB(value); }
        }

        public int Age { get; private set; }

        public void CalculateAge()
        {
            Age = DateTime.Now.Year - _dob.Year;
        }

        private void ConvertDOB(string sDOB)
        {
            try
            {
                //if (sDOB.Length != 12)
                //{
                //    return; // input length is not valid
                //}

                // Static
                if (sDOB.Length == 11)
                {
                    _dobInString = sDOB;
                }

                int yy = GetYear(sDOB);
                int mm = GetMonth(sDOB);
                int dd = GetDay(sDOB);

                if (yy == -1 || mm == -1 || dd == -1)
                {
                    return;
                }

                if (yy == -2 || mm == -2 || dd == -2)
                {
                    return;
                }

                if (mm > 12)
                {
                    return;
                }

                //_dob = new DateTime(yy, mm, dd);
                if (sDOB.Length == 12) //NRIC
                {
                    _dob = new DateTime(yy, mm, dd);
                }
                else if (sDOB.Length == 10)
                {
                    if (_dob.Year < 1900)
                    {
                        _dob = new DateTime(yy, mm, dd);
                    }
                    else if (!string.IsNullOrEmpty(_dobInString))
                    {
                        _dob = new DateTime(yy, mm, dd);
                        _dobInString = null;
                    }
                }
                else if (sDOB.Length == 11)
                {
                    // "dd/MM/yyyyu" - u : indicate for update
                    _dob = new DateTime(yy, mm, dd);
                }

                CalculateAge();
            }
            catch (Exception)
            {
                throw;
            }
        }


        private int GetYear(string dob)
        {
            int _year = -1;
            bool IsNumeric = false;

            if (dob.Length == 12 || dob.Length == 10 || dob.Length == 11)
            {
                if (dob.Length == 12)
                {
                    IsNumeric = int.TryParse(dob.Substring(0, 2), out _year);
                }
                else if (dob.Length == 10)
                {
                    IsNumeric = int.TryParse(dob.Substring(6, 4), out _year);

                    if (IsNumeric)
                    {
                        _year = Convert.ToInt32(_year.ToString().Substring(2, 2));
                    }
                }
                else if (dob.Length == 11)
                {
                    if (dob.Substring(10, 1) != "u")
                    {
                        return -2;
                    }

                    IsNumeric = int.TryParse(dob.Substring(6, 4), out _year);

                    if (IsNumeric)
                    {
                        _year = Convert.ToInt32(_year.ToString().Substring(2, 2));
                    }
                }
            }
            else
            {
                return -1; // input length is not valid
            }

            DateTime dt = DateTime.Now;
            int CurrentYear = Convert.ToInt32(dt.Year.ToString().Substring(2, 2));

            if (IsNumeric)
            {
                if (_year < CurrentYear)
                {
                    _year += 2000;
                }
                else
                {
                    _year += 1900;
                }
                return _year;
            }
            else
            {
                return -2; // format is wrong
            }
        }

        private int GetMonth(string dob)
        {
            int _Month = -1;
            bool IsNumeric = false;

            if (dob.Length == 12 || dob.Length == 10 || dob.Length == 11)
            {
                if (dob.Length == 12)
                {
                    IsNumeric = int.TryParse(dob.Substring(2, 2), out _Month);
                }
                else if (dob.Length == 10)
                {
                    IsNumeric = int.TryParse(dob.Substring(3, 2), out _Month);
                }
                else if (dob.Length == 11)
                {
                    if (dob.Substring(10, 1) != "u")
                    {
                        return -2;
                    }

                    IsNumeric = int.TryParse(dob.Substring(3, 2), out _Month);
                }
            }
            else
            {
                return -1; // input length is not valid
            }

            if (IsNumeric)
            {
                return _Month;
            }
            else
            {
                return -2; //format is wrong
            }
        }
        private int GetDay(string dob)
        {
            int _day = -1;
            bool IsNumeric = false;

            if (dob.Length == 12 || dob.Length == 10 || dob.Length == 11)
            {
                if (dob.Length == 12)
                {
                    IsNumeric = int.TryParse(dob.Substring(4, 2), out _day);
                }
                else if (dob.Length == 10)
                {
                    IsNumeric = int.TryParse(dob.Substring(0, 2), out _day);
                }
                else if (dob.Length == 11)
                {
                    if (dob.Substring(10, 1) != "u")
                    {
                        return -2;
                    }

                    IsNumeric = int.TryParse(dob.Substring(0, 2), out _day);
                }
            }
            else
            {
                return -1; // input length is not valid
            }

            if (IsNumeric)
            {
                return _day;
            }
            else
            {
                return -2; // format is wrong
            }
        }
    }
}

