import { useCallback } from 'react'
import ReactFlow, {
	Controls,
	Background,
	useNodesState,
	useEdgesState,
	addEdge,
	Edge,
	Connection,
	Panel,
} from 'reactflow'
import 'reactflow/dist/style.css'
import dagre from 'dagre'
import { Button } from '@mui/material'

const acceptanceColor = '#84DE02'
const rejectionColor = '#E32636'

const initialEdges = [
	{ id: 'e1-2', source: '1', target: '2' },
	{ id: 'e1-3', source: '1', target: '3', animated: true },
]

const initialNodes = [
	{
		id: '1',
		position: { x: 200, y: 200 },
		data: { label: '1' },
		style: {
			background: acceptanceColor,
			border: 'none',
		},
	},
	{
		id: '2',
		position: { x: 0, y: 300 },
		data: { label: '2' },
		style: {
			background: acceptanceColor,
			border: 'none',
		},
	},
	{
		id: '3',
		position: { x: 400, y: 300 },
		data: { label: '3' },
		style: {
			background: rejectionColor,
			border: 'none',
		},
	},
]

const dagreGraph = new dagre.graphlib.Graph()
dagreGraph.setDefaultEdgeLabel(() => ({}))

const nodeWidth = 172
const nodeHeight = 36

const getLayoutedElements = (nodes: any[], edges: any[], direction = 'TB') => {
	const isHorizontal = direction === 'LR'
	dagreGraph.setGraph({ rankdir: direction })

	nodes.forEach(node => {
		dagreGraph.setNode(node.id, { width: nodeWidth, height: nodeHeight })
	})

	edges.forEach(edge => {
		dagreGraph.setEdge(edge.source, edge.target)
	})

	dagre.layout(dagreGraph)

	nodes.forEach(node => {
		const nodeWithPosition = dagreGraph.node(node.id)
		node.targetPosition = isHorizontal ? 'left' : 'top'
		node.sourcePosition = isHorizontal ? 'right' : 'bottom'

		node.position = {
			x: nodeWithPosition.x - nodeWidth / 2,
			y: nodeWithPosition.y - nodeHeight / 2,
		}

		return node
	})

	return { nodes, edges }
}

getLayoutedElements(initialNodes, initialEdges)

const DataGraph = () => {
	const [nodes, setNodes, onNodesChange] = useNodesState(initialNodes)
	const [edges, setEdges, onEdgesChange] = useEdgesState(initialEdges)

	const onConnect = useCallback(
		(params: Edge | Connection) => setEdges(eds => addEdge(params, eds)),
		[setEdges]
	)

	const onLayout = useCallback(
		(direction: string | undefined) => {
			const { nodes: layoutedNodes, edges: layoutedEdges } =
				getLayoutedElements(nodes, edges, direction)

			setNodes([...layoutedNodes])
			setEdges([...layoutedEdges])
		},
		[nodes, edges, setNodes, setEdges]
	)

	return (
		<div
			style={{
				height: '100%',
				display: 'flex',
			}}
		>
			<ReactFlow
				nodes={nodes}
				edges={edges}
				onNodesChange={onNodesChange}
				onEdgesChange={onEdgesChange}
				onConnect={onConnect}
			>
				<Panel position='top-right'>
					<Button onClick={() => onLayout('TB')}>Вертикальный вид</Button>
					<Button onClick={() => onLayout('LR')}>Горизонтальный вид</Button>
				</Panel>
				<Controls />
				<Background gap={12} size={1} />
			</ReactFlow>
		</div>
	)
}

export default DataGraph
