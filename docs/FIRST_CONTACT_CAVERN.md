# First contact: the torch, the turn, the cavern

**Purpose:** let a visitor feel the scale of the Workshop before asking them to choose what they want to learn.

## The short journey

1. The Pages home starts in near-total darkness. One distant torch gives the eye a place to go.
2. The visitor chooses **Walk toward the light**. The passage brightens and approaches.
3. A quick bend in the hallway redirects the view.
4. The passage opens into a large cavern. Four clear doorways appear immediately: make something, operate a live system, push a boundary, or explore the package atlas.
5. **Skip to cavern** is available from the beginning. The visitor can go directly to the useful choices; the arrival is atmosphere, not a toll booth.
6. The arrival can be replayed from the learning page.

The visual currently uses a responsive SVG scene in the page. The arrival ambience is optional and generated in the browser after the visitor turns it on; there is no autoplay and no audio asset download.

## The acoustic side vault

The cavern includes an optional instrument based on the Physics repository's committed [first-order room-reflection experiment](https://github.com/TrentBest/TheSingularityWorkshop.Physics/blob/development/docs/experiments/first-order-room-reflection.md).

That controlled experiment uses a 10 m × 10 m × 10 m room, one source, one listener, and a single reflective wall. It records:

| Path | Distance | Arrival | Estimated level |
|---|---:|---:|---:|
| Direct | 6.3246 m | 18.4389 ms | 63.9794 dB SPL |
| One reflection | 10.1980 m | 29.7319 ms | 53.8091 dB SPL |

The **Hear the path pulse** control synthesizes two short noise pulses in the browser. Their scheduled arrivals and relative gain come from those committed Physics outputs. This makes the delay and level comparison audible without claiming the Physics package created the waveform.

### Evidence boundary

- **Physics model output:** source/listener path geometry, path lengths, delays, and per-path level estimates in the cited run record.
- **Browser audio preview:** a short generated pulse scheduled using the recorded direct/reflected path values.
- **Not modeled by this preview:** cavern geometry, a full room response, waveform propagation, phase/interference, absorption by frequency, scattering, diffraction, multiple-reflection reverberation, binaural perception, or material-driven sound generation.
- **Creative arrival ambience:** a separate optional browser-generated wind/torch texture. It is an authored atmospheric cue, not a Physics result.
- **Sound recipe direction:** [Experiences/Sound](https://github.com/TrentBest/TheSingularityWorkshop.Experiences/blob/master/experiences/Sound/README.md) is currently a design contract; it does not claim a complete SFX engine or executable sound MicroBundle. [The WebPage integration brief](https://github.com/TrentBest/WebPage/blob/development/docs/integrations/SINGULARITY_STUDIOS_SFX_WEBPAGELLM.md) describes the intended separation between creative sound and physical causality.

## Why this still belongs at the front door

The first seconds demonstrate an experience: composition, depth, light, movement, and a place to explore. The acoustic side vault then makes one actual Physics result tangible. The rest of the cavern points toward runnable systems and problem-focused plans.

The learning center should keep the immersive moment short, optional, and connected to useful next actions. A memorable entrance is only successful if the visitor can quickly leave it and get to what they came for.
