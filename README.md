# Planar Mirror Experiments

These are some experiments in Unity to do a planar mirror.
There are two techniques here:
- Dynamic reflection probes: terrible results, can probably be improved by moving the reflection probe relative to the camera
- Render target:
  - There's a version that is just setup by hand: the reflected camera is placed in the middle of the reflection object, at the same height as the provided original camera. The rotation of that camera is computed by reflecting the vector from the original camera to the mirror.
  - Auto mirror: Just a script you place in an object and it creates everything necessary for a mirror (needs to be provided a shader, there's one made with ShaderGraph, it just puts the _MainTex in the emissive channel). It automates what the the manual version does. Manual version is still useful if you want to setup extra parameters in the reflection camera, like culling masks

The render target version is still not perfect, there's still some distortion. A better version could build a cube map from the mirror viewpoint, and use a shader to do per-pixel reflection, but this is much more expensive, and in 
most cases the difference won't be that evident.

## License

[Apache 2.0](LICENSE)