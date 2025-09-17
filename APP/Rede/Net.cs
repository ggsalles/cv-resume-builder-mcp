using System;
using System.ComponentModel;
using System.Runtime.InteropServices;
using System.Windows.Forms;

public static class GerenciadorCredenciais
{
    [DllImport("advapi32.dll", SetLastError = true, CharSet = CharSet.Unicode)]
    private static extern bool CredRead(string targetName, uint type, uint reservedFlag, out IntPtr credentialPtr);

    [DllImport("advapi32.dll", SetLastError = true, CharSet = CharSet.Unicode)]
    private static extern bool CredWrite([In] ref CREDENTIAL userCredential, [In] uint flags);

    [DllImport("advapi32.dll", SetLastError = true, CharSet = CharSet.Unicode)]
    private static extern void CredFree([In] IntPtr buffer);

    [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Unicode)]
    private struct CREDENTIAL
    {
        public uint Flags;
        public uint Type;
        public string TargetName;
        public string Comment;
        public System.Runtime.InteropServices.ComTypes.FILETIME LastWritten;
        public uint CredentialBlobSize;
        public IntPtr CredentialBlob;
        public uint Persist;
        public uint AttributeCount;
        public IntPtr Attributes;
        public string TargetAlias;
        public string UserName;
    }

    // Verifica se a credencial já existe
    public static bool CredencialExiste(string nomeCredencial)
    {
        IntPtr credentialPtr = IntPtr.Zero;

        try
        {
            bool existe = CredRead(nomeCredencial, 1, 0, out credentialPtr);
            return existe;
        }
        catch
        {
            return false;
        }
        finally
        {
            if (credentialPtr != IntPtr.Zero)
                CredFree(credentialPtr);
        }
    }

    // Adiciona uma nova credencial
    public static bool AdicionarCredencial(string nomeCredencial, string usuario, string senha)
    {
        try
        {
            var credential = new CREDENTIAL
            {
                Type = 1, // CRED_TYPE_GENERIC
                TargetName = nomeCredencial,
                UserName = usuario,
                CredentialBlobSize = (uint)senha.Length * 2,
                CredentialBlob = Marshal.StringToCoTaskMemUni(senha),
                Persist = 2, // CRED_PERSIST_LOCAL_MACHINE
                Comment = "Credencial criada pela aplicação"
            };

            bool resultado = CredWrite(ref credential, 0);
            Marshal.FreeCoTaskMem(credential.CredentialBlob);

            if (!resultado)
                throw new Win32Exception(Marshal.GetLastWin32Error());

            return true;
        }
        catch (Exception ex)
        {
            MessageBox.Show($"Erro ao adicionar credencial: {ex.Message}");
            return false;
        }
    }

    // Método principal: verifica e adiciona se necessário
    public static bool VerificarEInserirCredencial(string nomeCredencial)
    {
        // Verifica se a credencial já existe
        if (CredencialExiste(nomeCredencial))
        {
            MessageBox.Show("Credencial já existe no Windows.");
            return true;
        }
        else
        {
            AdicionarCredencial(nomeCredencial, "Usuario", "Stock7!");
        }

            return false;
    }


}