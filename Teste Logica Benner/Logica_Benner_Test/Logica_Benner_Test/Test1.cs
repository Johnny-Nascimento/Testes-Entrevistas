using Teste_Logica;

namespace Logica_Benner_Test
{
    [TestClass]
    public sealed class Test1
    {
        [TestMethod]
        public void Deve_Retornar_Apenas_True()
        {
            // Arrange

            Arvore arvore = new Arvore(8);

            // Act

            arvore.Connect(1, 2);
            arvore.Connect(1, 6);
            arvore.Connect(6, 2);
            arvore.Connect(2, 4);
            arvore.Connect(5, 8);

            // Assert

            Assert.IsTrue(arvore.Query(1, 2));
            Assert.IsTrue(arvore.Query(1, 4));
            Assert.IsTrue(arvore.Query(6, 4));
            Assert.IsTrue(arvore.Query(1, 6));
            Assert.IsTrue(arvore.Query(6, 2));
            Assert.IsTrue(arvore.Query(2, 4));
            Assert.IsTrue(arvore.Query(5, 8));
        }

        [TestMethod]
        public void Deve_Retornar_Apenas_Falso()
        {
            // Arrange

            Arvore arvore = new Arvore(8);

            // Act

            arvore.Connect(1, 2);
            arvore.Connect(1, 6);
            arvore.Connect(6, 2);
            arvore.Connect(2, 4);
            arvore.Connect(5, 8);

            // Assert

            Assert.IsFalse(arvore.Query(7, 1));
            Assert.IsFalse(arvore.Query(7, 2));
            Assert.IsFalse(arvore.Query(7, 3));
            Assert.IsFalse(arvore.Query(7, 4));
            Assert.IsFalse(arvore.Query(7, 5));
            Assert.IsFalse(arvore.Query(7, 6));
            Assert.IsFalse(arvore.Query(7, 8));

            Assert.IsFalse(arvore.Query(3, 1));
            Assert.IsFalse(arvore.Query(3, 2));
            Assert.IsFalse(arvore.Query(3, 4));
            Assert.IsFalse(arvore.Query(3, 5));
            Assert.IsFalse(arvore.Query(3, 6));
            Assert.IsFalse(arvore.Query(3, 7));
            Assert.IsFalse(arvore.Query(3, 8));
        }

        [TestMethod]
        public void Deve_Retornar_Apenas_LevelConnection_0()
        {
            // Arrange

            Arvore arvore = new Arvore(8);

            // Act

            // Assert

            Assert.AreEqual(0, arvore.LevelConnection(1, 2));
            Assert.AreEqual(0, arvore.LevelConnection(2, 3));
            Assert.AreEqual(0, arvore.LevelConnection(3, 4));
            Assert.AreEqual(0, arvore.LevelConnection(4, 5));
            Assert.AreEqual(0, arvore.LevelConnection(6, 7));
            Assert.AreEqual(0, arvore.LevelConnection(7, 8));
        }

        [TestMethod]
        public void Deve_Retornar_Apenas_LevelConnection_1()
        {
            // Arrange

            Arvore arvore = new Arvore(8);

            // Act

            arvore.Connect(1, 2);
            arvore.Connect(3, 4);
            arvore.Connect(6, 7);

            // Assert

            Assert.AreEqual(1, arvore.LevelConnection(1, 2));
            Assert.AreEqual(1, arvore.LevelConnection(3, 4));
            Assert.AreEqual(1, arvore.LevelConnection(6, 7));
        }

        [TestMethod]
        public void Deve_Retornar_Apenas_LevelConnection_2()
        {
            // Arrange

            Arvore arvore = new Arvore(8);

            // Act

            arvore.Connect(1, 2);
            arvore.Connect(2, 3);
            arvore.Connect(3, 4);
            arvore.Connect(4, 5);
            arvore.Connect(5, 6);
            arvore.Connect(6, 7);
            arvore.Connect(7, 8);

            // Assert

            Assert.AreEqual(2, arvore.LevelConnection(1, 3));
            Assert.AreEqual(2, arvore.LevelConnection(2, 4));
            Assert.AreEqual(2, arvore.LevelConnection(3, 5));
            Assert.AreEqual(2, arvore.LevelConnection(4, 6));
            Assert.AreEqual(2, arvore.LevelConnection(5, 7));
            Assert.AreEqual(2, arvore.LevelConnection(6, 8));
            Assert.AreEqual(2, arvore.LevelConnection(8, 6));
        }

        [TestMethod]
        public void Deve_Retornar_Falso_Para_Os_Desconectados()
        {
            // Arrange

            Arvore arvore = new Arvore(8);

            arvore.Connect(1, 6);
            arvore.Connect(5, 8);

            // Act

            arvore.Disconnect(1, 6);
            arvore.Disconnect(5, 8);


            // Assert

            Assert.IsFalse(arvore.Query(1, 6));
            Assert.IsFalse(arvore.Query(5, 8));
        }

        [TestMethod]
        public void Deve_Retornar_LevellConnection_Com_Menor_Caminho()
        {
            // Arrange
            Arvore arvore = new Arvore(10);

            // Act 

            // 1 - 2 -3 - 4 - 5 - 6 - 7 - 8 - 9 - 10
            // |__________________|
            // Menor caminho 2 (6, 5)

            arvore.Connect(1, 2);
            arvore.Connect(2, 3);
            arvore.Connect(3, 4);
            arvore.Connect(4, 5);
            arvore.Connect(5, 6);
            arvore.Connect(6, 7);
            arvore.Connect(7, 8);

            arvore.Connect(1, 6);

            // Assert
            Assert.AreEqual(2, arvore.LevelConnection(1, 5));
        }

        [TestMethod]
        public void Deve_Retornar_LevellConnection_Variavel_Quantidade()
        {
            // Arrange
            int quantidade = 1000000;
            Arvore arvore = new Arvore(quantidade);

            // Act

            for (int i = 2; i <= quantidade; i++)
            {
                arvore.Connect(i - 1, i);
            }

            // Assert
            Assert.AreEqual(quantidade - 1, arvore.LevelConnection(1, quantidade));
        }
    }
}
