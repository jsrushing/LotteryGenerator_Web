using Microsoft.Ajax.Utilities;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data.Common;
using System.Linq;
using System.Net.Configuration;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace LotteryGenerator_Web
{
	public partial class Default : Page
	{
		StringBuilder SB = new StringBuilder();
		//private string[] row = new string[7];
		//private List<string> list = new List<string>();
		//private List<string[]> rows = new List<string[]>();

		protected void Page_Load(object sender, EventArgs e)
		{

		}

        protected void btnGenerate_Click(object sender, EventArgs e)
        {
			Random rnd = new Random();
			List<ListItem> list = new List<ListItem>();
			List<ListItem> subList = new List<ListItem>();

			var joinedNumber = string.Empty;
			var groupSizeCounter = 0;

			for(int i = 0; i < Convert.ToInt32(txtShuffle.Text); i++)
			{
				//PopulateTextBoxes(rnd);

				var output = GetFullNumber(rnd);	// TextBox1.Text + ":" + TextBox2.Text + ":" + TextBox3.Text + ":" + TextBox4.Text + ":" + TextBox5.Text + ":" + TextBox6.Text;
				var formattedOutput = string.Empty;

				foreach (var item in output.Split(':')) { formattedOutput += (item.Length == 1 ? "0" + item : item) + ":"; }
				
				groupSizeCounter++;
				joinedNumber = string.Join(":", formattedOutput.Substring(0, formattedOutput.Length - 1));
				list.Add(new ListItem(joinedNumber, ""));

				if (groupSizeCounter == Convert.ToInt32(txtGroupSize.Text)) 
				{
					var v = new ListItem(joinedNumber, "");
					subList.Add(v);
					groupSizeCounter = 0;
				}
			}

			//lblNumFound.Text = list.Count.ToString() + " raw items";
			lstFinal.Items.Clear();
			var vOutputCount = Convert.ToInt32(txtOutputNumber.Text);
			var subListCount = subList.Count > vOutputCount ? vOutputCount : subList.Count;

			if(subListCount > 0)
			{
				while(lstFinal.Items.Count < subListCount)
				{
					var v = subList.ToArray()[rnd.Next(1, subList.Count)];
					while (lstFinal.Items.Contains(v)) { v = new ListItem(GetFullNumber(rnd), ""); }
					lstFinal.Items.Add(v);
				}
			}

			//WindowsClipboard.SetText(list.ToArray());
		}

		private string GetFullNumber(Random rnd)
		{
			string sRtrn = string.Empty;
			var vNext = string.Empty;

			for(int i = 0; i < 6; i++)
			{
				vNext = rnd.Next(1, 100).ToString();
				if (vNext.Length == 1) vNext = "0" + vNext;
				sRtrn += vNext + ":";
			}

			return sRtrn.Substring(0, sRtrn.Length - 1);
		}

		protected bool IsNotUniqueInNumber(string value)
		{
			return TextBox1.Text == value | TextBox2.Text == value | TextBox3.Text == value 
				| TextBox4.Text == value | TextBox5.Text == value | TextBox6.Text == value;
		}

		private void PopulateTextBoxes(Random rnd)
		{
			//Random rnd = new Random();
			var next = rnd.Next(1, 100);
			TextBox1.Text = next.ToString();
			next = rnd.Next(1, 100);
			while (IsNotUniqueInNumber(next.ToString())) { next = rnd.Next(1, 100); }
			TextBox2.Text = next.ToString();
			next = rnd.Next(1, 100);
			while (IsNotUniqueInNumber(next.ToString())) { next = rnd.Next(1, 100); }
			TextBox3.Text = next.ToString();
			next = rnd.Next(1, 100);
			while (IsNotUniqueInNumber(next.ToString())) { next = rnd.Next(1, 100); }
			TextBox4.Text = next.ToString();
			next = rnd.Next(1, 100);
			while (IsNotUniqueInNumber(next.ToString())) { next = rnd.Next(1, 100); }
			TextBox5.Text = next.ToString();
			next = rnd.Next(1, 100);
			while (IsNotUniqueInNumber(next.ToString())) { next = rnd.Next(1, 100); }
			TextBox6.Text = next.ToString();
		}
	}

	/// <source>
	/// https://github.com/CopyText/TextCopy/blob/master/src/TextCopy/WindowsClipboard.cs
	/// https://stackoverflow.com/questions/44205260/net-core-copy-to-clipboard
	/// </source>
	static class WindowsClipboard
	{
		public static void SetText(string text)
		{
			OpenClipboard();

			EmptyClipboard();
			IntPtr hGlobal = default;
			try
			{
				var bytes = (text.Length + 1) * 2;
				hGlobal = Marshal.AllocHGlobal(bytes);

				if (hGlobal == default)
				{
					ThrowWin32();
				}

				var target = GlobalLock(hGlobal);

				if (target == default)
				{
					ThrowWin32();
				}

				try
				{
					Marshal.Copy(text.ToCharArray(), 0, target, text.Length);
				}
				finally
				{
					GlobalUnlock(target);
				}

				if (SetClipboardData(cfUnicodeText, hGlobal) == default)
				{
					ThrowWin32();
				}

				hGlobal = default;
			}
			finally
			{
				if (hGlobal != default)
				{
					Marshal.FreeHGlobal(hGlobal);
				}

				CloseClipboard();
			}
		}

		public static void OpenClipboard()
		{
			var num = 10;
			while (true)
			{
				if (OpenClipboard(default))
				{
					break;
				}

				if (--num == 0)
				{
					ThrowWin32();
				}

				Thread.Sleep(100);
			}
		}

		const uint cfUnicodeText = 13;

		static void ThrowWin32()
		{
			throw new Win32Exception(Marshal.GetLastWin32Error());
		}

		[DllImport("kernel32.dll", SetLastError = true)]
		static extern IntPtr GlobalLock(IntPtr hMem);

		[DllImport("kernel32.dll", SetLastError = true)]
		[return: MarshalAs(UnmanagedType.Bool)]
		static extern bool GlobalUnlock(IntPtr hMem);

		[DllImport("user32.dll", SetLastError = true)]
		[return: MarshalAs(UnmanagedType.Bool)]
		static extern bool OpenClipboard(IntPtr hWndNewOwner);

		[DllImport("user32.dll", SetLastError = true)]
		[return: MarshalAs(UnmanagedType.Bool)]
		static extern bool CloseClipboard();

		[DllImport("user32.dll", SetLastError = true)]
		static extern IntPtr SetClipboardData(uint uFormat, IntPtr data);

		[DllImport("user32.dll")]
		static extern bool EmptyClipboard();
	}
}