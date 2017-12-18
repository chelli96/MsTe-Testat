using System;
using System.Runtime.Serialization;
using System.Text;

namespace AutoReservation.Common.DataTransferObjects
{
	[DataContract]
    public class AutoDto : DtoBase<AutoDto>
    {
	    private int id;
	    private string marke;
	    private int basistarif;
	    private int tagestarif;
	    private AutoKlasse autoklasse;
	    private byte[] rowversion;

		[DataMember]
		public int Id
		{
			get
			{
				return id;
			}
			set
			{
				if (id != value)
				{
					id = value;
					OnPropertyChanged(nameof(Id));
				}
			}
		}

	    [DataMember]
	    public string Marke
	    {
		    get
		    {
			    return marke;
		    }
		    set
		    {
			    if (marke != value)
			    {
				    marke = value;
				    OnPropertyChanged(nameof(Marke));
			    }
		    }
	    }


	    [DataMember]
	    public int Tagestarif
	    {
		    get { return tagestarif; }
		    set
		    {
			    if (tagestarif != value)
			    {
				    tagestarif = value;
				    OnPropertyChanged(nameof(Tagestarif));
			    }
		    }
	    }

	    [DataMember]
	    public int Basistarif
	    {
		    get
		    {
			    return basistarif;
		    }
		    set
		    {
			    if (basistarif != value)
			    {
				    basistarif = value;
				    OnPropertyChanged(nameof(Basistarif));
			    }
		    }
	    }

	    [DataMember]
	    public AutoKlasse AutoKlasse
	    {
		    get
		    {
			    return autoklasse;
			    
		    }
		    set
		    {
			    if (autoklasse != value)
			    {
				    autoklasse = value;
					OnPropertyChanged(nameof(AutoKlasse));
			    }
		    }
	    }

		[DataMember]
		public byte[] RowVersion
		{
			get
			{
				return rowversion;
			}
			set
			{
				if (rowversion != value)
				{
					rowversion = value;
					OnPropertyChanged(nameof(RowVersion));
				}
			}
		}

		public override string Validate()
	    {
			StringBuilder error = new StringBuilder();
		    if (string.IsNullOrEmpty(Marke))
		    {
			    error.AppendLine("- Marke ist nicht gesetzt.");
		    }
		    if (Tagestarif <= 0)
		    {
			    error.AppendLine("- Tagestarif muss grösser als 0 sein.");
		    }
		    if (AutoKlasse == AutoKlasse.Luxusklasse && Basistarif <= 0)
		    {
			    error.AppendLine("- Basistarif eines Luxusautos muss grösser als 0 sein.");
		    }
		    if (error.Length == 0) { return null; }

		    return error.ToString();
		}


	    public override string ToString()
		   => $"{Id}; {Marke}; {Tagestarif}; {Basistarif}; {AutoKlasse}; {RowVersion}";
	}
}
