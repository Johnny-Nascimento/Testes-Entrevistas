using Teste_Logica_Benner.Testes;

namespace Logica_Benner_Test
{
    [TestClass]
    public sealed class Test2
    {
        [TestMethod]
        public void Deve_Retornar_Apenas_True()
        {
            // Arrange

            ArvoreEx ArvoreEx = new ArvoreEx(8);

            // Act

            ArvoreEx.Connect(1, 2);
            ArvoreEx.Connect(1, 6);
            ArvoreEx.Connect(6, 2);
            ArvoreEx.Connect(2, 4);
            ArvoreEx.Connect(5, 8);

            // Assert

            Assert.IsTrue(ArvoreEx.Query(1, 2));
            Assert.IsTrue(ArvoreEx.Query(1, 4));
            Assert.IsTrue(ArvoreEx.Query(6, 4));
            Assert.IsTrue(ArvoreEx.Query(1, 6));
            Assert.IsTrue(ArvoreEx.Query(6, 2));
            Assert.IsTrue(ArvoreEx.Query(2, 4));
            Assert.IsTrue(ArvoreEx.Query(5, 8));
        }

        [TestMethod]
        public void Deve_Retornar_Apenas_Falso()
        {
            // Arrange

            ArvoreEx ArvoreEx = new ArvoreEx(8);

            // Act

            ArvoreEx.Connect(1, 2);
            ArvoreEx.Connect(1, 6);
            ArvoreEx.Connect(6, 2);
            ArvoreEx.Connect(2, 4);
            ArvoreEx.Connect(5, 8);

            // Assert

            Assert.IsFalse(ArvoreEx.Query(7, 1));
            Assert.IsFalse(ArvoreEx.Query(7, 2));
            Assert.IsFalse(ArvoreEx.Query(7, 3));
            Assert.IsFalse(ArvoreEx.Query(7, 4));
            Assert.IsFalse(ArvoreEx.Query(7, 5));
            Assert.IsFalse(ArvoreEx.Query(7, 6));
            Assert.IsFalse(ArvoreEx.Query(7, 8));

            Assert.IsFalse(ArvoreEx.Query(3, 1));
            Assert.IsFalse(ArvoreEx.Query(3, 2));
            Assert.IsFalse(ArvoreEx.Query(3, 4));
            Assert.IsFalse(ArvoreEx.Query(3, 5));
            Assert.IsFalse(ArvoreEx.Query(3, 6));
            Assert.IsFalse(ArvoreEx.Query(3, 7));
            Assert.IsFalse(ArvoreEx.Query(3, 8));
        }

        [TestMethod]
        public void Deve_Retornar_Apenas_LevelConnection_0()
        {
            // Arrange

            ArvoreEx ArvoreEx = new ArvoreEx(8);

            // Act

            // Assert

            Assert.AreEqual(0, ArvoreEx.LevelConnection(1, 2));
            Assert.AreEqual(0, ArvoreEx.LevelConnection(2, 3));
            Assert.AreEqual(0, ArvoreEx.LevelConnection(3, 4));
            Assert.AreEqual(0, ArvoreEx.LevelConnection(4, 5));
            Assert.AreEqual(0, ArvoreEx.LevelConnection(6, 7));
            Assert.AreEqual(0, ArvoreEx.LevelConnection(7, 8));
        }

        [TestMethod]
        public void Deve_Retornar_Apenas_LevelConnection_1()
        {
            // Arrange

            ArvoreEx ArvoreEx = new ArvoreEx(8);

            // Act

            ArvoreEx.Connect(1, 2);
            ArvoreEx.Connect(3, 4);
            ArvoreEx.Connect(6, 7);

            // Assert

            Assert.AreEqual(1, ArvoreEx.LevelConnection(1, 2));
            Assert.AreEqual(1, ArvoreEx.LevelConnection(3, 4));
            Assert.AreEqual(1, ArvoreEx.LevelConnection(6, 7));
        }

        [TestMethod]
        public void Deve_Retornar_Apenas_LevelConnection_2()
        {
            // Arrange

            ArvoreEx ArvoreEx = new ArvoreEx(8);

            // Act

            ArvoreEx.Connect(1, 2);
            ArvoreEx.Connect(2, 3);
            ArvoreEx.Connect(3, 4);
            ArvoreEx.Connect(4, 5);
            ArvoreEx.Connect(5, 6);
            ArvoreEx.Connect(6, 7);
            ArvoreEx.Connect(7, 8);

            // Assert

            Assert.AreEqual(2, ArvoreEx.LevelConnection(1, 3));
            Assert.AreEqual(2, ArvoreEx.LevelConnection(2, 4));
            Assert.AreEqual(2, ArvoreEx.LevelConnection(3, 5));
            Assert.AreEqual(2, ArvoreEx.LevelConnection(4, 6));
            Assert.AreEqual(2, ArvoreEx.LevelConnection(5, 7));
            Assert.AreEqual(2, ArvoreEx.LevelConnection(6, 8));
            Assert.AreEqual(2, ArvoreEx.LevelConnection(8, 6));
        }

        [TestMethod]
        public void Deve_Retornar_Falso_Para_Os_Desconectados()
        {
            // Arrange

            ArvoreEx ArvoreEx = new ArvoreEx(8);

            ArvoreEx.Connect(1, 6);
            ArvoreEx.Connect(5, 8);

            // Act

            ArvoreEx.Disconnect(1, 6);
            ArvoreEx.Disconnect(5, 8);


            // Assert

            Assert.IsFalse(ArvoreEx.Query(1, 6));
            Assert.IsFalse(ArvoreEx.Query(5, 8));
        }

        [TestMethod]
        public void Deve_Retornar_LevellConnection_Com_Menor_Caminho()
        {
            // Arrange
            ArvoreEx ArvoreEx = new ArvoreEx(10);

            // Act 

            // 1 - 2 -3 - 4 - 5 - 6 - 7 - 8 - 9 - 10
            // |__________________|
            // Menor caminho 2 (6, 5)

            ArvoreEx.Connect(1, 2);
            ArvoreEx.Connect(2, 3);
            ArvoreEx.Connect(3, 4);
            ArvoreEx.Connect(4, 5);
            ArvoreEx.Connect(5, 6);
            ArvoreEx.Connect(6, 7);
            ArvoreEx.Connect(7, 8);

            ArvoreEx.Connect(1, 6);

            // Assert
            Assert.AreEqual(2, ArvoreEx.LevelConnection(1, 5));
        }

        [TestMethod]
        public void Deve_Retornar_LevellConnection_Variavel_Quantidade()
        {
            // Arrange
            int quantidade = 2000;
            ArvoreEx ArvoreEx = new ArvoreEx(quantidade);

            // Act

            for (int i = 2; i <= quantidade; i++)
            {
                ArvoreEx.Connect(i - 1, i);
            }

            // Assert
            Assert.AreEqual(quantidade - 1, ArvoreEx.LevelConnection(1, quantidade));
        }
    }
}
