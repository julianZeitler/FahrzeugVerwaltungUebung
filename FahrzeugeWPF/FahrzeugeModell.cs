using FahrzeugDatenbank;
using Fahrzeuge;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FahrzeugeWPF
{
    public class FahrzeugeModell
    {
        private readonly FahrzeugRepository _repository;

        public FahrzeugeModell(FahrzeugRepository repo)
        {
            this._repository = repo;
        }

        public async Task<IEnumerable<Fahrzeug>> LadeAlleFahrzeuge()
        {
            List<FahrzeugDTO>? fahrzeuge = await Task.Run(() =>
            {
                return _repository.HoleAlleFahrzeuge();
            });

            var fahrzeugeListe = KonvertiereFahrzeuge(fahrzeuge);
            return fahrzeugeListe;
        }

        private IEnumerable<Fahrzeug> KonvertiereFahrzeuge(IEnumerable<FahrzeugDTO> fahrzeuge)
        {
            return fahrzeuge.Select(fahrzeug => KonvertiereFahrzeug(fahrzeug));
        }

        private Fahrzeug KonvertiereFahrzeug(FahrzeugDTO fahrzeugDTO)
        {
            switch (fahrzeugDTO.Typ)
            {
                case "Auto":
                    var auto = new Auto()
                    { Id = fahrzeugDTO.Id, Name = fahrzeugDTO.Name };
                    return auto;

                case "Motorrad":
                    var motorrad = new Motorrad()
                    { Id = fahrzeugDTO.Id, Name = fahrzeugDTO.Name };
                    return motorrad;

                case "Fahrrad":
                    var fahrrad = new Fahrrad()
                    { Id = fahrzeugDTO.Id, Name = fahrzeugDTO.Name };
                    return fahrrad;
            }
            throw new Exception($"Unbekannter Fahrzeugtyp: {fahrzeugDTO.Typ}");
        }
    }
}
