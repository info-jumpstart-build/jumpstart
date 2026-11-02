// Mirrors web/blazor/Components/TabControl.razor.cshtml + TabItem.cs.cshtml
// (combined into one file -- TS has no reason to split an interface and its
// only consumer across two files the way the C# partial-class-per-file
// convention encourages). Tab switching is driven entirely by React state
// (onClick -> onTabChanged), same as the Blazor version's manual
// ActiveTab/StateHasChanged flow, so the Bootstrap JS tab plugin's
// data-bs-toggle attributes aren't needed here.
import "./TabControl.css";
import type { ReactNode } from "react";

export interface TabItem {
  title: string;
  content: ReactNode;
}

interface TabControlProps {
  id: string;
  tabs: TabItem[];
  activeTab: number;
  onTabChanged: (index: number) => void;
}

export default function TabControl({ id, tabs, activeTab, onTabChanged }: TabControlProps) {
  return (
    <div className="tab-container">
      <ul className="nav nav-tabs" id={id} role="tablist">
        {tabs.map((tab, index) => (
          <li className="nav-item" role="presentation" key={index}>
            <button
              className={`nav-link ${activeTab === index ? "active" : ""}`}
              id={`${id}-tab-${index}`}
              type="button"
              role="tab"
              aria-controls={`${id}-pane-${index}`}
              aria-selected={activeTab === index}
              onClick={() => onTabChanged(index)}
            >
              {tab.title}
            </button>
          </li>
        ))}
      </ul>

      <div className="tab-content" id={`${id}-content`}>
        {tabs.map((tab, index) => (
          <div
            className={`tab-pane fade ${activeTab === index ? "show active" : ""}`}
            id={`${id}-pane-${index}`}
            role="tabpanel"
            aria-labelledby={`${id}-tab-${index}`}
            key={index}
          >
            {tab.content}
          </div>
        ))}
      </div>
    </div>
  );
}
