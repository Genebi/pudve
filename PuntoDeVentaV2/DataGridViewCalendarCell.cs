using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace PuntoDeVentaV2
{
    public class DataGridViewCalendarCell : DataGridViewTextBoxCell
    {
        public DataGridViewCalendarCell()
        {
            this.Style.Format = "d"; // set the default date format
        }

        public override Type EditType
        {
            get
            {
                // Return the type of the editing control that DataGridViewCalendarCell uses.
                return typeof(DataGridViewCalendarEditingControl);
            }
        }

        public override void InitializeEditingControl(int rowIndex, object initialFormattedValue,
            DataGridViewCellStyle dataGridViewCellStyle)
        {
            // Set the value of the editing control to the value of the cell.
            base.InitializeEditingControl(rowIndex, initialFormattedValue, dataGridViewCellStyle);
            var editingControl = DataGridView.EditingControl as DataGridViewCalendarEditingControl;
            if (editingControl != null)
            {
                if (this.Value != null)
                {
                    DateTime dateTime;
                    if (DateTime.TryParse(this.Value.ToString(), out dateTime))
                    {
                        editingControl.Value = dateTime;
                    }
                }
            }
        }

        public override object ParseFormattedValue(object formattedValue, DataGridViewCellStyle cellStyle,
            TypeConverter formattedValueTypeConverter, TypeConverter valueTypeConverter)
        {
            // Return the value of the editing control as the new value of the cell.
            DateTime resultDateTime;
            if (DateTime.TryParse(formattedValue.ToString(), out resultDateTime))
            {
                return resultDateTime;
            }
            else
            {
                return DBNull.Value;
            }
        }

        public override bool KeyEntersEditMode(KeyEventArgs e)
        {
            // Start editing mode only when F2 key is pressed.
            if (e.KeyCode == Keys.F2 && !e.Alt && !e.Control && !e.Shift)
            {
                return true;
            }
            return base.KeyEntersEditMode(e);
        }

        public override Type ValueType
        {
            get
            {
                // Return the type of the value that DataGridViewCalendarCell contains.
                return typeof(DateTime);
            }
        }

        public override object DefaultNewRowValue
        {
            get
            {
                // Use the current date and time as the default value.
                return DateTime.Now;
            }
        }
    }

    public class DataGridViewCalendarEditingControl : DateTimePicker, IDataGridViewEditingControl
    {
        private DataGridView dataGridView;
        private bool valueChanged = false;
        private int rowIndex;

        public DataGridViewCalendarEditingControl()
        {
            this.Format = DateTimePickerFormat.Short;
        }

        public object EditingControlFormattedValue
        {
            get
            {
                return this.Value.ToShortDateString();
            }
            set
            {
                DateTime dateTime;
                if (DateTime.TryParse(value.ToString(), out dateTime))
                {
                    this.Value = dateTime;
                }
            }
        }

        public object GetEditingControlFormattedValue(DataGridViewDataErrorContexts context)
        {
            return EditingControlFormattedValue;
        }

        public void ApplyCellStyleToEditingControl(DataGridViewCellStyle dataGridViewCellStyle)
        {
            this.Font = dataGridViewCellStyle.Font;
            this.CalendarForeColor = dataGridViewCellStyle.ForeColor;
            this.CalendarMonthBackground = dataGridViewCellStyle.BackColor;
        }

        public int EditingControlRowIndex
        {
            get
            {
                return rowIndex;
            }
            set
            {
                rowIndex = value;
            }
        }

        public bool EditingControlWantsInputKey(Keys key, bool dataGridViewWantsInputKey)
        {
            switch (key & Keys.KeyCode)
            {
                case Keys.Left:
                case Keys.Up:
                case Keys.Down:
                case Keys.Right:
                case Keys.Home:
                case Keys.End:
                case Keys.PageDown:
                case Keys.PageUp:
                    return true;
                default:
                    return !dataGridViewWantsInputKey;
            }
        }

        public void PrepareEditingControlForEdit(bool selectAll)
        {
            // No preparation needs to be done.
        }

        public bool RepositionEditingControlOnValueChange
        {
            get
            {
                return false;
            }
        }

        public DataGridView EditingControlDataGridView
        {
            get
            {
                return dataGridView;
            }
            set
            {
                dataGridView = value;
            }
        }

        public bool EditingControlValueChanged
        {
            get
            {
                return valueChanged;
            }
            set
            {
                valueChanged = value;
            }
        }

        public Cursor EditingPanelCursor
        {
            get
            {
                return base.Cursor;
            }
        }

        protected override void OnValueChanged(EventArgs eventargs)
        {
            // Notify the DataGridView that the contents of the cell have changed.
            valueChanged = true;
            this.EditingControlDataGridView.NotifyCurrentCellDirty(true);
            base.OnValueChanged(eventargs);
        }
    }



}
