//  Created by Matt Purchase.
//  Copyright (c) 2021 Matt Purchase. All rights reserved.
using System;
using System.Collections.Generic;
using UnityEngine;

#if UNITY_EDITOR
using UnityEditor;
#endif


public class DrawGizmo : MonoBehaviour {
	// Properties
	public List<GizmoConstruct> m_gizmos;
	public n_gizmoDrawType m_drawType;

	public DrawGizmo() {
		if (m_gizmos == null) {
			m_gizmos = new List<GizmoConstruct>();
			m_gizmos.Add(new GizmoConstruct());
		}
	}

	public void SetScale(float scale) {
		for (int a = 0; a < m_gizmos.Count; a++) {
			GizmoConstruct giz = m_gizmos[a];
			giz.m_scale = scale;
		}
	}

	public void AddGizmo(GizmoConstruct construct) {
		m_gizmos.Add(construct);
	}

#if UNITY_EDITOR

	void OnDrawGizmosSelected() {
		if (m_drawType == n_gizmoDrawType.selected) {
			Draw();
		}
	}

	void OnDrawGizmos() {
		if (m_drawType == n_gizmoDrawType.always) {
			Draw();
		}
	}


	private void Draw() {

		foreach (GizmoConstruct construct in m_gizmos) {
			Handles.color = construct.m_color;
			Gizmos.color = construct.m_color;

			if (construct.m_isVisible == false) {
				continue;
			}

			switch (construct.m_drawStyle) {
				case (n_gizmoDrawStyle.cube): {
						if (construct.m_renderStyle == n_renderingStyle.wire) {
							Gizmos.DrawWireCube(transform.position, new Vector3(construct.m_scale, construct.m_scale, construct.m_scale));
						}
						else {
							Gizmos.DrawCube(transform.position, new Vector3(construct.m_scale, construct.m_scale, construct.m_scale));
						}
						break;
					}
				case (n_gizmoDrawStyle.sphere): {
						if (construct.m_renderStyle == n_renderingStyle.wire) {
							Gizmos.DrawWireSphere(transform.position, construct.m_scale);
						}
						else {
							Gizmos.DrawSphere(transform.position, construct.m_scale);
						}
						break;
					}
				case (n_gizmoDrawStyle.circle): {
						if (construct.m_renderStyle == n_renderingStyle.wire) {
							Handles.DrawWireDisc(transform.position, Vector3.up, construct.m_scale);
						}
						else {
							Handles.DrawSolidDisc(transform.position, Vector3.up, construct.m_scale);
						}
						break;
					}
				case (n_gizmoDrawStyle.arrow): {
						if (construct.m_renderStyle == n_renderingStyle.wire) {
							// Handles.DrawWireDisc(transform.position, Vector3.up, construct.m_scale);
							DrawArrow(construct.m_scale, construct.m_color);
						}
						else {
							// Handles.Draw(transform.position, Vector3.up, construct.m_scale);
							DrawArrow(construct.m_scale, construct.m_color);
						}
						break;
					}
				case (n_gizmoDrawStyle.square): {
						if (construct.m_renderStyle == n_renderingStyle.wire) {
							// Handles.DrawWireDisc(transform.position, Vector3.up, construct.m_scale);
							Gizmos.DrawWireCube(transform.position, new Vector3(construct.m_scale, 0.01f, construct.m_scale));
						}
						else {
							// Handles.Draw(transform.position, Vector3.up, construct.m_scale);
							Gizmos.DrawCube(transform.position, new Vector3(construct.m_scale, 0.01f, construct.m_scale));
						}
						break;
					}
				case (n_gizmoDrawStyle.text): {
						GUIStyle style = new GUIStyle();
						style.normal.textColor = construct.m_color;
						style.fontSize = Mathf.CeilToInt(construct.m_scale);
						if (construct.m_usesGameObjectName) {
							Handles.Label(transform.position, gameObject.name, style);
						}
						else {
							Handles.Label(transform.position, construct.m_text);
						}
						break;
					}
			}
		}
	}


	public void DrawArrow(float scale, Color col) {
		Vector3[] arrowHead = new Vector3[3];
		Vector3[] arrowLine = new Vector3[2];
		Vector3 start = transform.position;
		Vector3 end = transform.position + transform.forward * scale;


		Vector3 forward = (end - start).normalized;
		Vector3 right = Vector3.Cross(Vector3.up, forward).normalized;
		float size = HandleUtility.GetHandleSize(end);
		float width = size * 0.1f;
		float height = size * 0.3f;

		arrowHead[0] = end;
		arrowHead[1] = end - forward * height + right * width;
		arrowHead[2] = end - forward * height - right * width;

		arrowLine[0] = start;
		arrowLine[1] = end - forward * height;

		Handles.color = col;
		Handles.DrawAAPolyLine(arrowLine);
		Handles.DrawAAConvexPolygon(arrowHead);
	}



#endif
}

[Serializable]
public class GizmoConstruct {
	public bool m_isVisible = true;
	public n_gizmoDrawStyle m_drawStyle;
	public n_renderingStyle m_renderStyle;
	public UnityEngine.Color m_color = Color.green;
	public float m_scale = 0.25f;
	public string m_text;
	public bool m_usesGameObjectName;
}

public enum n_gizmoDrawType {
	selected,
	always,
}

public enum n_gizmoDrawStyle {
	cube,
	sphere,
	circle,
	text,
	arrow,
	square,
}

public enum n_renderingStyle {
	wire,
	solid,
}