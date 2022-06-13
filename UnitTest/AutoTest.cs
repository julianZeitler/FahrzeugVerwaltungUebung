using Xunit;

namespace UnitTest
{
    public class AutoTest
    {
        [Fact]
        public void SetzeAnzahlRaederPosTest()
        {
            var auto = new Fahrzeuge.Auto();

            const int expected = 2;

            auto.SetzeAnzahlRaeder(expected);

            int result = auto.AnzahlRaeder;

            Assert.Equal(expected, result);
        }

        [Fact]
        public void SetzeAnzahlRaederNegTest()
        {
            var auto = new Fahrzeuge.Auto();

            int expected = auto.AnzahlRaeder;

            auto.SetzeAnzahlRaeder(-4);

            int result = auto.AnzahlRaeder;

            Assert.Equal(expected, result);
        }
    }
}