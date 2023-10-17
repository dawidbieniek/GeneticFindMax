namespace AE1
{
	partial class MainForm
	{
		/// <summary>
		///  Required designer variable.
		/// </summary>
		private System.ComponentModel.IContainer components = null;

		/// <summary>
		///  Clean up any resources being used.
		/// </summary>
		/// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
		protected override void Dispose(bool disposing)
		{
			if (disposing && (components != null))
			{
				components.Dispose();
			}
			base.Dispose(disposing);
		}

		#region Windows Form Designer generated code

		/// <summary>
		///  Required method for Designer support - do not modify
		///  the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{
			left_layout = new FlowLayoutPanel();
			functionGraph_PLACEHOLDER = new Panel();
			function_container = new TableLayoutPanel();
			function_title = new Label();
			function_entry = new TextBox();
			functionOk_button = new Button();
			xRange_container = new TableLayoutPanel();
			xRange_title = new Label();
			xTo_label = new Label();
			xTo_entry = new TextBox();
			xFrom_entry = new TextBox();
			xFrom_label = new Label();
			bottom_layout = new FlowLayoutPanel();
			algGraph_PLACEHOLDER = new Panel();
			mainDivide_layout = new TableLayoutPanel();
			right_layout = new FlowLayoutPanel();
			start_button = new Button();
			algStats_container = new TableLayoutPanel();
			iteration_displayLabel = new Label();
			iteration_title = new Label();
			avg_title = new Label();
			min_title = new Label();
			min_displayLabel = new Label();
			max_title = new Label();
			avg_displayLabel = new Label();
			max_displayLabel = new Label();
			middle_layout = new FlowLayoutPanel();
			algParams_container = new TableLayoutPanel();
			population_title = new Label();
			population_entry = new TextBox();
			mutProb_title = new Label();
			mutProb_entry = new TextBox();
			crossProb_title = new Label();
			crossProb_entry = new TextBox();
			pkPercent_label = new Label();
			pmPercent_label = new Label();
			left_layout.SuspendLayout();
			function_container.SuspendLayout();
			xRange_container.SuspendLayout();
			bottom_layout.SuspendLayout();
			mainDivide_layout.SuspendLayout();
			right_layout.SuspendLayout();
			algStats_container.SuspendLayout();
			middle_layout.SuspendLayout();
			algParams_container.SuspendLayout();
			SuspendLayout();
			// 
			// left_layout
			// 
			left_layout.Controls.Add(functionGraph_PLACEHOLDER);
			left_layout.Dock = DockStyle.Fill;
			left_layout.FlowDirection = FlowDirection.TopDown;
			left_layout.Location = new Point(10, 10);
			left_layout.Margin = new Padding(0);
			left_layout.Name = "left_layout";
			left_layout.Size = new Size(259, 238);
			left_layout.TabIndex = 0;
			// 
			// functionGraph_PLACEHOLDER
			// 
			functionGraph_PLACEHOLDER.BackColor = SystemColors.ControlDark;
			functionGraph_PLACEHOLDER.Location = new Point(3, 3);
			functionGraph_PLACEHOLDER.Name = "functionGraph_PLACEHOLDER";
			functionGraph_PLACEHOLDER.Size = new Size(200, 200);
			functionGraph_PLACEHOLDER.TabIndex = 0;
			// 
			// function_container
			// 
			function_container.AutoSize = true;
			function_container.ColumnCount = 2;
			function_container.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
			function_container.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 40F));
			function_container.Controls.Add(function_title, 0, 0);
			function_container.Controls.Add(function_entry, 0, 1);
			function_container.Controls.Add(functionOk_button, 1, 1);
			function_container.Location = new Point(3, 3);
			function_container.Name = "function_container";
			function_container.RowCount = 2;
			function_container.RowStyles.Add(new RowStyle(SizeType.Absolute, 25F));
			function_container.RowStyles.Add(new RowStyle(SizeType.Absolute, 25F));
			function_container.Size = new Size(245, 50);
			function_container.TabIndex = 1;
			// 
			// function_title
			// 
			function_title.AutoSize = true;
			function_title.Location = new Point(3, 3);
			function_title.Margin = new Padding(3);
			function_title.Name = "function_title";
			function_title.Size = new Size(25, 15);
			function_title.TabIndex = 0;
			function_title.Text = "f(x)";
			// 
			// function_entry
			// 
			function_entry.Location = new Point(0, 25);
			function_entry.Margin = new Padding(0, 0, 5, 0);
			function_entry.Name = "function_entry";
			function_entry.Size = new Size(200, 23);
			function_entry.TabIndex = 1;
			// 
			// functionOk_button
			// 
			functionOk_button.Location = new Point(205, 25);
			functionOk_button.Margin = new Padding(0);
			functionOk_button.Name = "functionOk_button";
			functionOk_button.Size = new Size(40, 23);
			functionOk_button.TabIndex = 2;
			functionOk_button.Text = "Ok";
			functionOk_button.UseVisualStyleBackColor = true;
			// 
			// xRange_container
			// 
			xRange_container.ColumnCount = 4;
			xRange_container.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 25F));
			xRange_container.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 75F));
			xRange_container.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 25F));
			xRange_container.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 75F));
			xRange_container.Controls.Add(xRange_title, 0, 0);
			xRange_container.Controls.Add(xTo_label, 2, 1);
			xRange_container.Controls.Add(xTo_entry, 3, 1);
			xRange_container.Controls.Add(xFrom_entry, 1, 1);
			xRange_container.Controls.Add(xFrom_label, 0, 1);
			xRange_container.Location = new Point(3, 59);
			xRange_container.Name = "xRange_container";
			xRange_container.RowCount = 2;
			xRange_container.RowStyles.Add(new RowStyle(SizeType.Absolute, 25F));
			xRange_container.RowStyles.Add(new RowStyle(SizeType.Absolute, 25F));
			xRange_container.RowStyles.Add(new RowStyle(SizeType.Absolute, 20F));
			xRange_container.RowStyles.Add(new RowStyle(SizeType.Absolute, 20F));
			xRange_container.Size = new Size(200, 50);
			xRange_container.TabIndex = 2;
			// 
			// xRange_title
			// 
			xRange_title.AutoSize = true;
			xRange_container.SetColumnSpan(xRange_title, 4);
			xRange_title.Location = new Point(3, 3);
			xRange_title.Margin = new Padding(3);
			xRange_title.Name = "xRange_title";
			xRange_title.Size = new Size(50, 15);
			xRange_title.TabIndex = 0;
			xRange_title.Text = "Zakres x";
			// 
			// xTo_label
			// 
			xTo_label.Anchor = AnchorStyles.Left;
			xTo_label.AutoSize = true;
			xTo_label.Location = new Point(103, 30);
			xTo_label.Margin = new Padding(3, 0, 0, 0);
			xTo_label.Name = "xTo_label";
			xTo_label.Size = new Size(21, 15);
			xTo_label.TabIndex = 1;
			xTo_label.Text = "do";
			// 
			// xTo_entry
			// 
			xTo_entry.Dock = DockStyle.Fill;
			xTo_entry.Location = new Point(125, 25);
			xTo_entry.Margin = new Padding(0);
			xTo_entry.Name = "xTo_entry";
			xTo_entry.Size = new Size(75, 23);
			xTo_entry.TabIndex = 3;
			// 
			// xFrom_entry
			// 
			xFrom_entry.Dock = DockStyle.Fill;
			xFrom_entry.Location = new Point(25, 25);
			xFrom_entry.Margin = new Padding(0);
			xFrom_entry.Name = "xFrom_entry";
			xFrom_entry.Size = new Size(75, 23);
			xFrom_entry.TabIndex = 4;
			// 
			// xFrom_label
			// 
			xFrom_label.Anchor = AnchorStyles.Left;
			xFrom_label.AutoSize = true;
			xFrom_label.Location = new Point(3, 30);
			xFrom_label.Margin = new Padding(3, 0, 0, 0);
			xFrom_label.Name = "xFrom_label";
			xFrom_label.Size = new Size(21, 15);
			xFrom_label.TabIndex = 2;
			xFrom_label.Text = "od";
			// 
			// bottom_layout
			// 
			mainDivide_layout.SetColumnSpan(bottom_layout, 3);
			bottom_layout.Controls.Add(algGraph_PLACEHOLDER);
			bottom_layout.Dock = DockStyle.Fill;
			bottom_layout.Location = new Point(10, 248);
			bottom_layout.Margin = new Padding(0);
			bottom_layout.Name = "bottom_layout";
			bottom_layout.Size = new Size(780, 238);
			bottom_layout.TabIndex = 2;
			// 
			// algGraph_PLACEHOLDER
			// 
			algGraph_PLACEHOLDER.BackColor = SystemColors.ControlDark;
			algGraph_PLACEHOLDER.Location = new Point(3, 3);
			algGraph_PLACEHOLDER.Name = "algGraph_PLACEHOLDER";
			algGraph_PLACEHOLDER.Size = new Size(774, 235);
			algGraph_PLACEHOLDER.TabIndex = 1;
			// 
			// mainDivide_layout
			// 
			mainDivide_layout.ColumnCount = 3;
			mainDivide_layout.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 33.3333321F));
			mainDivide_layout.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 33.3333321F));
			mainDivide_layout.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 33.3333321F));
			mainDivide_layout.Controls.Add(right_layout, 2, 0);
			mainDivide_layout.Controls.Add(middle_layout, 1, 0);
			mainDivide_layout.Controls.Add(bottom_layout, 0, 1);
			mainDivide_layout.Controls.Add(left_layout, 0, 0);
			mainDivide_layout.Dock = DockStyle.Fill;
			mainDivide_layout.Location = new Point(0, 0);
			mainDivide_layout.Margin = new Padding(0);
			mainDivide_layout.Name = "mainDivide_layout";
			mainDivide_layout.Padding = new Padding(10);
			mainDivide_layout.RowCount = 3;
			mainDivide_layout.RowStyles.Add(new RowStyle(SizeType.Percent, 50F));
			mainDivide_layout.RowStyles.Add(new RowStyle(SizeType.Percent, 50F));
			mainDivide_layout.RowStyles.Add(new RowStyle(SizeType.Absolute, 20F));
			mainDivide_layout.Size = new Size(800, 516);
			mainDivide_layout.TabIndex = 0;
			// 
			// right_layout
			// 
			right_layout.Controls.Add(start_button);
			right_layout.Controls.Add(algStats_container);
			right_layout.Dock = DockStyle.Fill;
			right_layout.FlowDirection = FlowDirection.TopDown;
			right_layout.Location = new Point(531, 13);
			right_layout.Name = "right_layout";
			right_layout.Size = new Size(256, 232);
			right_layout.TabIndex = 4;
			// 
			// start_button
			// 
			start_button.Location = new Point(3, 3);
			start_button.Name = "start_button";
			start_button.Size = new Size(75, 23);
			start_button.TabIndex = 0;
			start_button.Text = "Start";
			start_button.UseVisualStyleBackColor = true;
			// 
			// algStats_container
			// 
			algStats_container.AutoSize = true;
			algStats_container.ColumnCount = 2;
			algStats_container.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 70F));
			algStats_container.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 100F));
			algStats_container.Controls.Add(iteration_displayLabel, 1, 0);
			algStats_container.Controls.Add(iteration_title, 0, 0);
			algStats_container.Controls.Add(avg_title, 0, 2);
			algStats_container.Controls.Add(min_title, 0, 1);
			algStats_container.Controls.Add(min_displayLabel, 1, 1);
			algStats_container.Controls.Add(max_title, 0, 3);
			algStats_container.Controls.Add(avg_displayLabel, 1, 2);
			algStats_container.Controls.Add(max_displayLabel, 1, 3);
			algStats_container.Location = new Point(3, 32);
			algStats_container.Name = "algStats_container";
			algStats_container.RowCount = 4;
			algStats_container.RowStyles.Add(new RowStyle(SizeType.Absolute, 25F));
			algStats_container.RowStyles.Add(new RowStyle(SizeType.Absolute, 25F));
			algStats_container.RowStyles.Add(new RowStyle(SizeType.Absolute, 25F));
			algStats_container.RowStyles.Add(new RowStyle(SizeType.Absolute, 25F));
			algStats_container.RowStyles.Add(new RowStyle(SizeType.Absolute, 20F));
			algStats_container.Size = new Size(170, 100);
			algStats_container.TabIndex = 1;
			// 
			// iteration_displayLabel
			// 
			iteration_displayLabel.Anchor = AnchorStyles.Left;
			iteration_displayLabel.AutoSize = true;
			iteration_displayLabel.Location = new Point(73, 6);
			iteration_displayLabel.Margin = new Padding(3, 3, 3, 0);
			iteration_displayLabel.Name = "iteration_displayLabel";
			iteration_displayLabel.Size = new Size(13, 15);
			iteration_displayLabel.TabIndex = 9;
			iteration_displayLabel.Text = "0";
			// 
			// iteration_title
			// 
			iteration_title.Anchor = AnchorStyles.Left;
			iteration_title.AutoSize = true;
			iteration_title.Location = new Point(3, 6);
			iteration_title.Margin = new Padding(3, 3, 3, 0);
			iteration_title.Name = "iteration_title";
			iteration_title.Size = new Size(45, 15);
			iteration_title.TabIndex = 8;
			iteration_title.Text = "iteracja";
			// 
			// avg_title
			// 
			avg_title.Anchor = AnchorStyles.Left;
			avg_title.AutoSize = true;
			avg_title.Location = new Point(3, 56);
			avg_title.Margin = new Padding(3, 3, 3, 0);
			avg_title.Name = "avg_title";
			avg_title.Size = new Size(26, 15);
			avg_title.TabIndex = 2;
			avg_title.Text = "avg";
			// 
			// min_title
			// 
			min_title.Anchor = AnchorStyles.Left;
			min_title.AutoSize = true;
			min_title.Location = new Point(3, 31);
			min_title.Margin = new Padding(3, 3, 3, 0);
			min_title.Name = "min_title";
			min_title.Size = new Size(28, 15);
			min_title.TabIndex = 0;
			min_title.Text = "min";
			// 
			// min_displayLabel
			// 
			min_displayLabel.Anchor = AnchorStyles.Left;
			min_displayLabel.AutoSize = true;
			min_displayLabel.Location = new Point(73, 31);
			min_displayLabel.Margin = new Padding(3, 3, 3, 0);
			min_displayLabel.Name = "min_displayLabel";
			min_displayLabel.Size = new Size(13, 15);
			min_displayLabel.TabIndex = 5;
			min_displayLabel.Text = "0";
			// 
			// max_title
			// 
			max_title.Anchor = AnchorStyles.Left;
			max_title.AutoSize = true;
			max_title.Location = new Point(3, 81);
			max_title.Margin = new Padding(3, 3, 3, 0);
			max_title.Name = "max_title";
			max_title.Size = new Size(30, 15);
			max_title.TabIndex = 4;
			max_title.Text = "max";
			// 
			// avg_displayLabel
			// 
			avg_displayLabel.Anchor = AnchorStyles.Left;
			avg_displayLabel.AutoSize = true;
			avg_displayLabel.Location = new Point(73, 56);
			avg_displayLabel.Margin = new Padding(3, 3, 3, 0);
			avg_displayLabel.Name = "avg_displayLabel";
			avg_displayLabel.Size = new Size(13, 15);
			avg_displayLabel.TabIndex = 6;
			avg_displayLabel.Text = "0";
			// 
			// max_displayLabel
			// 
			max_displayLabel.Anchor = AnchorStyles.Left;
			max_displayLabel.AutoSize = true;
			max_displayLabel.Location = new Point(73, 81);
			max_displayLabel.Margin = new Padding(3, 3, 3, 0);
			max_displayLabel.Name = "max_displayLabel";
			max_displayLabel.Size = new Size(13, 15);
			max_displayLabel.TabIndex = 7;
			max_displayLabel.Text = "0";
			// 
			// middle_layout
			// 
			middle_layout.Controls.Add(function_container);
			middle_layout.Controls.Add(xRange_container);
			middle_layout.Controls.Add(algParams_container);
			middle_layout.Dock = DockStyle.Fill;
			middle_layout.Location = new Point(272, 13);
			middle_layout.Name = "middle_layout";
			middle_layout.Size = new Size(253, 232);
			middle_layout.TabIndex = 3;
			// 
			// algParams_container
			// 
			algParams_container.AutoSize = true;
			algParams_container.ColumnCount = 3;
			algParams_container.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 70F));
			algParams_container.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 100F));
			algParams_container.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 30F));
			algParams_container.Controls.Add(population_title, 0, 2);
			algParams_container.Controls.Add(population_entry, 1, 2);
			algParams_container.Controls.Add(mutProb_title, 0, 1);
			algParams_container.Controls.Add(mutProb_entry, 1, 1);
			algParams_container.Controls.Add(crossProb_title, 0, 0);
			algParams_container.Controls.Add(crossProb_entry, 1, 0);
			algParams_container.Controls.Add(pkPercent_label, 2, 0);
			algParams_container.Controls.Add(pmPercent_label, 2, 1);
			algParams_container.Location = new Point(3, 115);
			algParams_container.Name = "algParams_container";
			algParams_container.RowCount = 3;
			algParams_container.RowStyles.Add(new RowStyle(SizeType.Absolute, 25F));
			algParams_container.RowStyles.Add(new RowStyle(SizeType.Absolute, 25F));
			algParams_container.RowStyles.Add(new RowStyle(SizeType.Absolute, 25F));
			algParams_container.Size = new Size(200, 75);
			algParams_container.TabIndex = 0;
			// 
			// population_title
			// 
			population_title.Anchor = AnchorStyles.Left;
			population_title.AutoSize = true;
			population_title.Location = new Point(3, 56);
			population_title.Margin = new Padding(3, 3, 3, 0);
			population_title.Name = "population_title";
			population_title.Size = new Size(59, 15);
			population_title.TabIndex = 4;
			population_title.Text = "populacja";
			// 
			// population_entry
			// 
			population_entry.Dock = DockStyle.Fill;
			population_entry.Location = new Point(70, 50);
			population_entry.Margin = new Padding(0);
			population_entry.Name = "population_entry";
			population_entry.Size = new Size(100, 23);
			population_entry.TabIndex = 5;
			// 
			// mutProb_title
			// 
			mutProb_title.Anchor = AnchorStyles.Left;
			mutProb_title.AutoSize = true;
			mutProb_title.Location = new Point(3, 31);
			mutProb_title.Margin = new Padding(3, 3, 3, 0);
			mutProb_title.Name = "mutProb_title";
			mutProb_title.Size = new Size(30, 15);
			mutProb_title.TabIndex = 2;
			mutProb_title.Text = "p_m";
			// 
			// mutProb_entry
			// 
			mutProb_entry.Dock = DockStyle.Fill;
			mutProb_entry.Location = new Point(70, 25);
			mutProb_entry.Margin = new Padding(0);
			mutProb_entry.Name = "mutProb_entry";
			mutProb_entry.Size = new Size(100, 23);
			mutProb_entry.TabIndex = 3;
			// 
			// crossProb_title
			// 
			crossProb_title.Anchor = AnchorStyles.Left;
			crossProb_title.AutoSize = true;
			crossProb_title.Location = new Point(3, 6);
			crossProb_title.Margin = new Padding(3, 3, 3, 0);
			crossProb_title.Name = "crossProb_title";
			crossProb_title.Size = new Size(25, 15);
			crossProb_title.TabIndex = 0;
			crossProb_title.Text = "p_k";
			// 
			// crossProb_entry
			// 
			crossProb_entry.Dock = DockStyle.Fill;
			crossProb_entry.Location = new Point(70, 0);
			crossProb_entry.Margin = new Padding(0);
			crossProb_entry.Name = "crossProb_entry";
			crossProb_entry.Size = new Size(100, 23);
			crossProb_entry.TabIndex = 1;
			// 
			// pkPercent_label
			// 
			pkPercent_label.Anchor = AnchorStyles.Left;
			pkPercent_label.AutoSize = true;
			pkPercent_label.Location = new Point(173, 6);
			pkPercent_label.Margin = new Padding(3, 3, 3, 0);
			pkPercent_label.Name = "pkPercent_label";
			pkPercent_label.Size = new Size(17, 15);
			pkPercent_label.TabIndex = 6;
			pkPercent_label.Text = "%";
			// 
			// pmPercent_label
			// 
			pmPercent_label.Anchor = AnchorStyles.Left;
			pmPercent_label.AutoSize = true;
			pmPercent_label.Location = new Point(173, 31);
			pmPercent_label.Margin = new Padding(3, 3, 3, 0);
			pmPercent_label.Name = "pmPercent_label";
			pmPercent_label.Size = new Size(17, 15);
			pmPercent_label.TabIndex = 7;
			pmPercent_label.Text = "%";
			// 
			// MainForm
			// 
			AutoScaleDimensions = new SizeF(7F, 15F);
			AutoScaleMode = AutoScaleMode.Font;
			ClientSize = new Size(800, 516);
			Controls.Add(mainDivide_layout);
			MaximizeBox = false;
			Name = "MainForm";
			ShowIcon = false;
			Text = "Algorytmy ewolucyjne - max funkcji";
			left_layout.ResumeLayout(false);
			function_container.ResumeLayout(false);
			function_container.PerformLayout();
			xRange_container.ResumeLayout(false);
			xRange_container.PerformLayout();
			bottom_layout.ResumeLayout(false);
			mainDivide_layout.ResumeLayout(false);
			right_layout.ResumeLayout(false);
			right_layout.PerformLayout();
			algStats_container.ResumeLayout(false);
			algStats_container.PerformLayout();
			middle_layout.ResumeLayout(false);
			middle_layout.PerformLayout();
			algParams_container.ResumeLayout(false);
			algParams_container.PerformLayout();
			ResumeLayout(false);
		}

		#endregion
		private FlowLayoutPanel left_layout;
		private Panel functionGraph_PLACEHOLDER;
		private TableLayoutPanel function_container;
		private Label function_title;
		private TextBox function_entry;
		private FlowLayoutPanel bottom_layout;
		private TableLayoutPanel mainDivide_layout;
		private TableLayoutPanel xRange_container;
		private Label xFrom_label;
		private Label xRange_title;
		private Label xTo_label;
		private TextBox xTo_entry;
		private TextBox xFrom_entry;
		private FlowLayoutPanel right_layout;
		private FlowLayoutPanel middle_layout;
		private TableLayoutPanel algParams_container;
		private Label population_title;
		private TextBox population_entry;
		private Label mutProb_title;
		private TextBox mutProb_entry;
		private Label crossProb_title;
		private TextBox crossProb_entry;
		private Button start_button;
		private TableLayoutPanel algStats_container;
		private Label avg_title;
		private Label min_title;
		private Label min_displayLabel;
		private Label max_title;
		private Label avg_displayLabel;
		private Label max_displayLabel;
		private Label iteration_displayLabel;
		private Label iteration_title;
		private Panel algGraph_PLACEHOLDER;
		private Button functionOk_button;
		private Label pkPercent_label;
		private Label pmPercent_label;
	}
}
