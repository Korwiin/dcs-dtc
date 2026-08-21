using System.Net.Sockets;

namespace DTC.Utilities.Network;

internal class PresetDataSender
{
    public static void Send(string str)
    {
        try
        {
            using (var tcpClient = new TcpClient("127.0.0.1", Settings.TCPSendPort))
            using (var ns = tcpClient.GetStream())
            using (var sw = new StreamWriter(ns))
            {
                //System.Diagnostics.Debug.WriteLine(str);
                File.WriteAllText(Path.Combine(Path.GetTempPath(), "dtc-last-command.lua"), str); // TEMP DEBUG - remove before PR
                sw.WriteLine(str);
                sw.Flush();
            }
        }
        catch (SocketException)
        {
            DTCMessageBox.ShowError("Error connecting to DCS. Make sure DTC is installed correctly, DCS is running and you are in the cockpit.");
        }
    }
}