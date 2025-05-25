# 2nd Trabalho TDJ

Este projeto é um jogo de plataforma 2D desenvolvido em **MonoGame** com suporte a:
- Tilemaps criados no [Tiled](https://www.mapeditor.org/)
- Animações de personagem
- Sistema de inimigos com patrulhamento
- Coleta de moedas
- Colisões com o cenário
- Câmera que segue o jogador
- Detecção de fim de jogo

---

## 🎮 Controles

| Tecla | Ação             |
|-------|------------------|
| A;D   | Mover jogador    |
|Espaço | Saltar           |
| J     | Ataque do jogador|
| Esc   | Sair do jogo     |

---

## 🧱 Funcionalidades

- 🧍 Jogador com animações de andar, saltar, cair e atacar(só aparece a hitbox e faz a animaçao, ainda não têm utilidade)
- 👾 Inimigos com rotas de patrulha e detecção de colisão com o jogador
- 💰 Moedas espalhadas pelo mapa com sistema de coleta
- 🗺️ Tilemaps importados do **Tiled** (.tmx)
- 🧱 Colisões baseadas em objetos do mapa
- 🎥 Câmera que acompanha o jogador
- 🚪 Ponto de início (SP) e fim (EP) do nível definidos no Tiled

---

## 📦 Requisitos

- [.NET 6+](https://dotnet.microsoft.com/)
- [MonoGame 3.8+](https://www.monogame.net/)
- [TiledSharp](https://github.com/marshallward/TiledSharp)
- [FontStashSharp](https://github.com/rds1983/FontStashSharp)
- [Apos.Gui](https://github.com/Apostolique/Apos.Gui) (opcional)

---


## 🧠 Lógica do Jogo

### 🎮 Jogador
- Controlado via teclado
- Interage com o ambiente e inimigos(apenas na Consola)
- Sofre dano ao tocar em inimigos(Apenas visivel na Consola)
- Morre ao perder toda a vida(Apenas visivel na Consola)

### 👾 Inimigos
- Movem-se dentro de regiões pré-definidas (paths no Tiled)
- Causam dano por contato(Apenas Visivel na Consola)

### 💰 Moedas
- Definidas por objetos no Tiled (camada "Coins")
- Coletadas ao colidir com o jogador
- Incrementam a pontuação(Apenas visivel na Consola)

### 🧱 Colisões
- Colisões com o cenário baseadas na camada "colisions" do Tiled

---
## 📋 Notas

- A lógica da colisão pode ser mais refinada.
- A HUD e sistema de níveis futuros podem ser adicionados facilmente.
- O projeto ainda pode ser melhorado com sons, animações, checkpoints e outras mecânicas.

---
