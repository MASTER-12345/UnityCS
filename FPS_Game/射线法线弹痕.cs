当射击到一个物体上时，

如果能留下弹痕，会感觉比较真实。

弹痕可以用一张 贴图来实现，

有点像生活中的什么呢？

有点像 生活中的贴纸，假设我们有一张贴纸，这张贴纸可以贴在各个地方，

unity <wbr> <wbr>弹痕效果的一种实现方法

我们可以想象成，我们有很多 弹孔的贴纸，我们将其贴在各种物体上，

如果细分，可以有 金属的弹孔贴纸， 木头的弹孔贴纸，玻璃窗的弹孔贴纸。

在某种程度上，我们可以将 弹孔看成是一个 片，可以贴在 各个物体的表面上。

plane或quad ，这个可以让特效来自己选择如何实现，特效可以将击中的效果和弹痕放在一起，

做成一个特效，当击中某个物体的时候，就选择相应的特效，作为子物体，添加在碰撞的物体上。

这是原理，

现在，需要解决两个问题，

第一个问题是，贴在什么位置。
第二个问题是，角度如何。

也就是说，我们需要知道 碰撞的点，还有碰撞的角度，

这些怎么计算呢？

unity 已经提供了这些功能，可以这样写，

Ray ray = aimCamera.ScreenPointToRay(new Vector3(Screen.width*0.5f,Screen.height*0.5f,0));

RaycastHit hitInfo;

if (Physics.Raycast(ray, out hitInfo))


现在 ，碰撞的信息，就都在 hitInfo中了，

hitInfo.point就是碰撞的点，
hitInfo.normal就是碰撞的方向，

这里是一个 Vector3 向量，而不是 Quaternion ,

根据 向量 转成 Quaternion可以这样，

Quaternion.LookRotation(Vector3) ;

假如，特效在制作的时候，并不是用 unity 默认的forwad方向来制作，

而是用 垂直向上的方向来做的，

可以这样转化一下

Quaternion.FromToRotation (Vector3.forward, hitInfo.normal)

碰撞的物体可以这样获取

hitInfo.transform

现在，我们知道了，碰撞了谁，

碰撞的位置，角度，

将这个特效添加在物体上，弹痕效果就实现了。

这里，可以设定一个回收时间，例如，多久要删除，

或者在某种情况下删除，例如，最多30个弹痕，多了之后，最先添加的就会最早删除。

