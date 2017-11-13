using System;
using System.Collections.Generic;
using Xamarin.Forms;

namespace Calendar
{
    public partial class CalendarControl : ContentView
    {
        protected DateTime CurrentDate { get; private set; }

        protected List<Label> TitleLabels;

        protected StackLayout AppointmentLayout { get; private set; }

        public CalendarControl()
        {
            TitleLabels = new List<Label>(7);

            CurrentDate = new DateTime(DateTime.Today.Year, DateTime.Today.Month, 1);

            InitializeComponent();

            Content = CreateMainView();

        }

        protected Color ContentViewBorderColor { get { return new Color(0, 0, 0); } }

        protected Color MonthAndYearViewBackgroundColor { get { return new Color(255, 255, 255); } }
        protected Color MonthAndYearTextColor { get { return new Color(0, 0, 0); } }
        protected Color MonthAndYearBackgroundColor { get { return new Color(255, 255, 255); } }
        protected Color MonthAndYearButtonTextColor { get { return new Color(0, 0, 0); } }
        protected Color MonthAndYearButtonBackgroundColor { get { return new Color(255, 255, 255); } }

        protected Color WeekDayEmptyBackgroundColor { get { return Color.FromHex("#ddd"); } }
        protected Color WeekDayBackgroundColor { get { return Color.FromHex("#c6c6c6"); } }
        protected Color WeekDayTitleBackgroundColor { get { return new Color(255, 255, 255); } }

        protected Color LabelWeekDayTextColor { get { return new Color(0, 0, 0); } }
        protected Color LabelDayBackgroundColor { get { return Color.FromRgba(255, 255, 255, 0); } }
        protected Color LabelDayTextColor { get { return new Color(0, 0, 0); } }

        protected Color AppointmentLayoutBackgroundColor { get { return Color.FromRgba(255, 255, 255, 0); } }

        protected double MonthAndYearTextFontSize { get { return Device.GetNamedSize(NamedSize.Small, typeof(Button)); } }
        protected double MonthAndYearButtonFontSize { get { return Device.GetNamedSize(NamedSize.Large, typeof(Button)); } }
        protected double WeekDayFontSize { get { return Device.GetNamedSize(NamedSize.Medium, typeof(Button)); } }
        protected double DayFontSize { get { return Device.GetNamedSize(NamedSize.Medium, typeof(Button)); } }

        protected Grid CreateMainView() {
            var columDef = new ColumnDefinition { Width = new GridLength(1, GridUnitType.Star) };
            var rowDef1 = new RowDefinition { Height = GridLength.Auto };
            var rowDef2 = new RowDefinition { Height = new GridLength(1, GridUnitType.Star) };

            Grid GridMainView = new Grid {
                BackgroundColor = ContentViewBorderColor,
                ColumnDefinitions = new ColumnDefinitionCollection { columDef },
                RowDefinitions = new RowDefinitionCollection { rowDef1, rowDef2 },
                ColumnSpacing = 1,
                RowSpacing = 1
            };
            GridMainView.Children.Add(CreateTitleView(), 0, 0);
            GridMainView.Children.Add(CreateContentView(), 0, 1);

            return GridMainView;
        }

        protected Grid CreateTitleView() {
            var columDef = new ColumnDefinition { Width = new GridLength(1, GridUnitType.Star) };
            var rowDef = new RowDefinition { Height = GridLength.Auto };
            var buttonBack = new Button
            {
                TextColor = MonthAndYearButtonTextColor,
                BackgroundColor = MonthAndYearButtonBackgroundColor,
                FontSize = MonthAndYearButtonFontSize,
                Text = "<"
            };
            buttonBack.Clicked += ButtonBack_Clicked;

            var buttonFoward = new Button
            {
                TextColor = MonthAndYearButtonTextColor,
                BackgroundColor = MonthAndYearButtonBackgroundColor,
                FontSize = MonthAndYearButtonFontSize,
                Text = ">"
            };
            buttonFoward.Clicked += ButtonFoward_Clicked;

            var labelTitle = new Label {
                VerticalTextAlignment = TextAlignment.Center,
                HorizontalTextAlignment = TextAlignment.Center,
                TextColor = MonthAndYearTextColor,
                BackgroundColor = MonthAndYearBackgroundColor,
                FontSize = MonthAndYearTextFontSize,
                Text = SetMonthAndYear(),
            };

            Grid GridTitle = new Grid
            {
                BackgroundColor = MonthAndYearViewBackgroundColor,
                ColumnDefinitions = new ColumnDefinitionCollection { columDef, columDef, columDef, columDef, columDef, columDef, columDef },
                RowDefinitions = new RowDefinitionCollection { rowDef },
                ColumnSpacing = 1,
                RowSpacing = 1
            };
            GridTitle.Children.Add(buttonBack, 1, 0);
            GridTitle.Children.Add(buttonFoward, 5, 0);
            GridTitle.Children.Add(labelTitle, 2, 5, 0, 1);

            return GridTitle;
        }

        protected int SetStartDay() {
            var startDay = 0;
            switch (CurrentDate.DayOfWeek.ToString().ToLower())
            {
                case "sunday":
                    startDay = 0;
                    break;
                case "monday":
                    startDay = 1;
                    break;
                case "tuesday":
                    startDay = 2;
                    break;
                case "wednesday":
                    startDay = 3;
                    break;
                case "thursday":
                    startDay = 4;
                    break;
                case "friday":
                    startDay = 5;
                    break;
                case "saturday":
                    startDay = 6;
                    break;
            }
            return startDay;
        }

        protected Grid CreateContentView()
        {
            var columDef = new ColumnDefinition { Width = new GridLength(1, GridUnitType.Star) };
            var rowDefWeekDay = new RowDefinition { Height = GridLength.Auto };
            var rowDefDays = new RowDefinition { Height = GridLength.Star };
            var startDay = SetStartDay();
            var calendarDay = 1;
            var MonthLastDay = DateTime.DaysInMonth(CurrentDate.Year, CurrentDate.Month);  //CurrentDate.AddMonths(1).AddDays(-1);
            var column = 0;
            var row = 0;
            var gridSize = 35;

            Grid GridCalendar = new Grid
            {
                BackgroundColor = ContentViewBorderColor,
                ColumnDefinitions = new ColumnDefinitionCollection { columDef, columDef, columDef, columDef, columDef, columDef, columDef },
                RowDefinitions = new RowDefinitionCollection { rowDefWeekDay, rowDefDays, rowDefDays, rowDefDays, rowDefDays, rowDefDays },
                ColumnSpacing = 1,
                RowSpacing = 1
            };

            do
            {
                var box = new BoxView
                {
                    BackgroundColor = WeekDayEmptyBackgroundColor
                };
                GridCalendar.Children.Add(box, column, row);
                if (row == 0) {
                    var title = new Label
                    {
                        VerticalTextAlignment = TextAlignment.Center,
                        HorizontalTextAlignment = TextAlignment.Center,
                        TextColor = LabelWeekDayTextColor,
                        BackgroundColor = WeekDayTitleBackgroundColor,
                        //HorizontalOptions = LayoutOptions.FillAndExpand,
                        //VerticalOptions = LayoutOptions.FillAndExpand,
                        FontSize = WeekDayFontSize,
                        //FontAttributes = NumberOfWeekFontAttributes,
                        //FontFamily = NumberOfWeekFontFamily
                    };
                    title.Text = SetWeekDay(column + 1);
                    TitleLabels.Add(title);
                    GridCalendar.Children.Add(title, column, row);
                }
                else {
                    if ( ((row == 1) && (column >= startDay)) || (row > 1) )
                    {
                        if (calendarDay <= MonthLastDay)
                            GridCalendar.Children.Add(CreateWeekDay(calendarDay), column, row);
                        calendarDay++;
                    } 

                }
                column++;
                if (column >= 7)
                    row++;
                if (column % 7 == 0)
                    column = 0;

            } while (calendarDay <= (gridSize - startDay));

            return GridCalendar;
        }

        protected Grid CreateWeekDay(int calendarDay) {
            var columDef = new ColumnDefinition { Width = new GridLength(1, GridUnitType.Star) };
            var rowDef = new RowDefinition { Height = GridLength.Auto };
            var day = new Label 
            { 
                HorizontalTextAlignment = TextAlignment.End, 
                TextColor = LabelDayTextColor, 
                Text = calendarDay.ToString(),
                FontSize = DayFontSize,
            };
            day.BackgroundColor = LabelDayBackgroundColor;
            Grid GridWeek = new Grid
            {
                BackgroundColor = WeekDayBackgroundColor,
                ColumnDefinitions = new ColumnDefinitionCollection { columDef },
                RowDefinitions = new RowDefinitionCollection { rowDef, rowDef },
                ColumnSpacing = 0,
                RowSpacing = 0,
                Padding = new Thickness(1, 1, 1, 1),
                Margin = new Thickness(0, 0, 0, 0),
            };

            this.AppointmentLayout = new StackLayout
            {
                BackgroundColor = AppointmentLayoutBackgroundColor,
                HorizontalOptions = LayoutOptions.Fill,
                VerticalOptions = LayoutOptions.Start,
                Orientation = StackOrientation.Vertical,
                Spacing = 1,
                Padding = new Thickness(0, 0, 0, 0),
                Margin = new Thickness(5, 0, 0, 0),
            };

            GridWeek.Children.Add(day, 0, 0);
            GridWeek.Children.Add(AppointmentLayout, 0, 1);

            return GridWeek;
        }

        public void AddAppointment(Appointment appoint)
        {
            BoxView Appointment = new BoxView
            {
                BackgroundColor = GetColor(appoint),
                HorizontalOptions = LayoutOptions.Start,
                VerticalOptions = LayoutOptions.Start,
                HeightRequest = 10,
                WidthRequest = 10
            };

            this.AppointmentLayout.Children.Add(Appointment);

        }

        private Color GetColor(Appointment appoint)
        {
            Color aColor;

            switch (appoint.Type) {
                case Appointment.AppointmentType.Bath:
                    aColor = Color.FromHex("#f00");
                    break;
                case Appointment.AppointmentType.Ectoparasite:
                    aColor = Color.FromHex("#0f0");
                    break;
                case Appointment.AppointmentType.Medicine:
                    aColor = Color.FromHex("#00f");
                    break;
                case Appointment.AppointmentType.Other:
                    aColor = Color.FromHex("#f0f");
                    break;
                case Appointment.AppointmentType.Shopping:
                    aColor = Color.FromHex("#ff0");
                    break;
                case Appointment.AppointmentType.Vaccine:
                    aColor = Color.FromHex("#0ff");
                    break;
                case Appointment.AppointmentType.Vermifuge:
                    aColor = Color.FromHex("#f88");
                    break;
                case Appointment.AppointmentType.Veterinarian:
                    aColor = Color.FromHex("88f");
                    break;
                default: 
                    aColor = Color.DarkTurquoise;
                    break;
            }

            return aColor;
        }

        protected string SetWeekDay(int day) {
            var nameDay = "";
            switch (day) {
                case 1:
                    nameDay = "Dom";
                    break;
                case 2:
                    nameDay = "Seg";
                    break;
                case 3:
                    nameDay = "Ter";
                    break;
                case 4:
                    nameDay = "Qua";
                    break;
                case 5:
                    nameDay = "Qui";
                    break;
                case 6:
                    nameDay = "Sex";
                    break;
                case 7:
                    nameDay = "Sab";
                    break;
            }
            return nameDay;
        }

        protected string SetMonthAndYear()
        {
            string month = "";
            switch (CurrentDate.Month)
            {
                case 1:
                    month = "Janeiro";
                    break;
                case 2:
                    month = "Fevereiro";
                    break;
                case 3:
                    month = "Março";
                    break;
                case 4:
                    month = "Abril";
                    break;
                case 5:
                    month = "Maio";
                    break;
                case 6:
                    month = "Junho";
                    break;
                case 7:
                    month = "Julho";
                    break;
                case 8:
                    month = "Agosto";
                    break;
                case 9:
                    month = "Setembro";
                    break;
                case 10:
                    month = "Outubro";
                    break;
                case 11:
                    month = "Novembro";
                    break;
                case 12:
                    month = "Dezembro";
                    break;
            }

            return (month.ToUpper() + " " + CurrentDate.Year.ToString());

        }

        void ButtonBack_Clicked(object sender, EventArgs e)
        {
            CurrentDate = CurrentDate.AddMonths(-1);
            Content = CreateMainView();
        }

        void ButtonFoward_Clicked(object sender, EventArgs e)
        {
            CurrentDate = CurrentDate.AddMonths(1);
            Content = CreateMainView();
        }

    }
}
