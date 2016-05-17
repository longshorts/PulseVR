Reflex gun sight shader v 1.3
-----------------------------

Description:
------------

	This shader accurately recreates the effect of a reflector - or 'reflex' - optical gun sight.
	A reflex sight works by projecting a reticle to infinity, which allows accurate aiming without
	needing to align the eye with the sight. The shader is perfect for modern combat themed FPS
	games or flight sims (for a heads-up display).
	
	The package includes a reflex sight shader, a reflex shader for angled surfaces, and a holographic
	shader. A holographic sight differs from a reflex sight in that the reticle is set at a	finite
	distance, which reduces parallax. Versions of each shader with cubemap reflections are also
	included.

	The shader is highly customizable; when applied to a material, the reticle image, colour,
	brightness and scale can be modified, as well as the sight window transparency and colour.
	The desired viewing angle for the angled reflex shader, reticle distance for the holographic
	shader, and the reflection map and colour for the reflective shaders can also be modified.

	The package includes 8 shader files, 7 reticle textures, 8 reticle materials and 3 demo scenes.

Changelog:
----------

v 1.0:
------
	Initial version.
	
v 1.1:
------
	Added "rotated" shader and a copy of each shader file with distance scaling removed. Added new
	reticle, new material and new demo scene for "rotated" shader.
	
v 1.2:
------
	Added a holographic shader. This shader differs from the reflex shader in that the reticle is at
	a finite distance (which can be set as a parameter). The sight is then effectively parallax free
	at the specified distance, instead of only at infinity. Optimized the rotated shader to run on SM2
	graphics cards. Added reflective (cubemap) versions of each shader. Added another demo scene with
	the reflective holographic shader.
	
v 1.3:
------
	Fixed a bug in the holographic shader that would cause the reticle to not behave correctly when the
	sight was moved.
	
Instructions:
-------------

	To use these shaders, import both your sight model and the shader package into Unity, then either
	apply one of the included materials to your model, or create a new material, select one of the
	shaders	from Transparent/Reflex Sight, set the reticle and other parameters as desired, and then
	apply to your model.

	Here are a couple of things to note when using these shaders:

1)	The shader assumes that the surface to which it is applied lies in the XY plane in local space.
	It will not work correctly when applied to surfaces that do not lie in this plane. If you have
	transformed your model in your modeling program so that the surface lies in the XY plane (in world
	space), don't forget to collapse your transforms before exporting to Unity. Alternately, just perform
	your transformations in local space.
	
2)	You can make your own reticles quite easily. Just create a square PNG texture with all opaque
	sections having an alpha value of 1, and all transparent sections having an alpha value of 0.
	I'd suggest downloading GIMP (a free image manipulation program) and googling for some tutorials
	about transparency and alpha. When importing the reticle texture into Unity, make sure to set the
	wrap mode to clamp.

3)	Using the "rotated" shaders is similar to using the regular ones, except that the sight must be
	rotated to the same angle as specified in the associated shader parameter in world space (or the
	viewing angle to the sight must be the same value) for the reticle to be visible. Please note that
	the surface of the sight should still lie in the XY plane in local space.
	
4)	The holographic sight shader not only requires that the sight surface lies in the XY plane, but also
	that the sight surface is centred at 0,0 in the XY plane. If it is not, the reticle will not appear
	in the center of the sight.

	Lastly, thanks for buying this package!
