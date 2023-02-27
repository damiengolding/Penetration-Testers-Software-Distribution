/* 
   Copyright (C) Damien Golding
   
   This is free software; you can redistribute it and/or
   modify it under the terms of the GNU Library General Public
   License as published by the Free Software Foundation; either
   version 2 of the License, or (at your option) any later version.
   
   This library is distributed in the hope that it will be useful,
   but WITHOUT ANY WARRANTY; without even the implied warranty of
   MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the GNU
   Library General Public License for more details.
   
   You should have received a copy of the GNU Library General Public
   License along with this software; if not, write to the Free
   Software Foundation, Inc., 59 Temple Place - Suite 330, Boston,
   MA 02111-1307, USA
*/

namespace PTRPConfigurator {
	partial class ConfigWizard {
		/// <summary>
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.IContainer components = null;

		/// <summary>
		/// Clean up any resources being used.
		/// </summary>
		/// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
		protected override void Dispose( bool disposing ) {
			if (disposing && (components != null)) {
				components.Dispose();
			}
			base.Dispose(disposing);
		}

		#region Windows Form Designer generated code

		/// <summary>
		/// Required method for Designer support - do not modify
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent() {
			this.advWizard = new AdvancedWizardControl.Wizard.AdvancedWizard();
			this.FirstPage = new AdvancedWizardControl.WizardPages.AdvancedWizardPage();
			this.advWizard.SuspendLayout();
			this.SuspendLayout();
			// 
			// advWizard
			// 
			this.advWizard.BackButtonEnabled = false;
			this.advWizard.BackButtonText = "< Back";
			this.advWizard.ButtonLayout = AdvancedWizardControl.Enums.ButtonLayoutKind.Default;
			this.advWizard.ButtonsVisible = true;
			this.advWizard.CancelButtonText = "&Cancel";
			this.advWizard.Controls.Add(this.FirstPage);
			this.advWizard.CurrentPageIsFinishPage = false;
			this.advWizard.Dock = System.Windows.Forms.DockStyle.Fill;
			this.advWizard.FinishButton = true;
			this.advWizard.FinishButtonEnabled = true;
			this.advWizard.FinishButtonText = "&Finish";
			this.advWizard.FlatStyle = System.Windows.Forms.FlatStyle.Standard;
			this.advWizard.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.advWizard.HelpButton = true;
			this.advWizard.HelpButtonText = "&Help";
			this.advWizard.Location = new System.Drawing.Point(0, 0);
			this.advWizard.Name = "advWizard";
			this.advWizard.NextButtonEnabled = false;
			this.advWizard.NextButtonText = "Next >";
			this.advWizard.ProcessKeys = false;
			this.advWizard.Size = new System.Drawing.Size(419, 239);
			this.advWizard.TabIndex = 0;
			this.advWizard.TouchScreen = false;
			this.advWizard.WizardPages.Add(this.FirstPage);
			// 
			// FirstPage
			// 
			this.FirstPage.Dock = System.Windows.Forms.DockStyle.Fill;
			this.FirstPage.Header = true;
			this.FirstPage.HeaderBackgroundColor = System.Drawing.Color.White;
			this.FirstPage.HeaderFont = new System.Drawing.Font("Verdana", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.FirstPage.HeaderImage = global::PTRPConfigurator.Properties.Resources.logo;
			this.FirstPage.HeaderImageVisible = true;
			this.FirstPage.HeaderTitle = "Pen Test Result Parser";
			this.FirstPage.Location = new System.Drawing.Point(0, 0);
			this.FirstPage.Name = "FirstPage";
			this.FirstPage.PreviousPage = 0;
			this.FirstPage.Size = new System.Drawing.Size(419, 199);
			this.FirstPage.SubTitle = "Package configuration";
			this.FirstPage.SubTitleFont = new System.Drawing.Font("Tahoma", 8F);
			this.FirstPage.TabIndex = 1;
			this.FirstPage.Paint += new System.Windows.Forms.PaintEventHandler(this.advancedWizardPage1_Paint);
			// 
			// ConfigWizard
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(419, 239);
			this.Controls.Add(this.advWizard);
			this.Name = "ConfigWizard";
			this.Text = "PTRP Configuration";
			this.Load += new System.EventHandler(this.ConfigWizard_Load);
			this.advWizard.ResumeLayout(false);
			this.ResumeLayout(false);

		}

		#endregion

		private AdvancedWizardControl.Wizard.AdvancedWizard advWizard;
		private AdvancedWizardControl.WizardPages.AdvancedWizardPage FirstPage;
	}
}

