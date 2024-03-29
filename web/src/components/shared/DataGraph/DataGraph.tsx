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
import { Box } from '@mui/material'
import { GrayButton } from '@/components/ui/button/GrayButton'
import styles from './DataGraph.module.scss'

const acceptanceColor = '#54C16C'
const rejectionColor = '#E25E63'
const commentsColor = '#EBB855'
const acceptanceWithCommentsColor = '#aac954'
const notStartedColor = '#ececec'
const sentVerificationColor = '#5dc7de'
const takenVerificationColor = '#5c98d0'
const blockColor = '#25c8aa'

const edgeType = 'smoothstep'
const position = { x: 0, y: 0 }

const initialEdges = [
	{ id: 'e1-2', source: '1', target: '2', type: edgeType },
	{ id: 'e1-3', source: '1', target: '3', animated: false, type: edgeType },
	{ id: 'e2-4', source: '2', target: '4', animated: false, type: edgeType },
	{ id: 'e2-5', source: '2', target: '5', animated: false, type: edgeType },
	{ id: 'e3-6', source: '3', target: '6', type: edgeType },
	{ id: 'e3-7', source: '3', target: '7', type: edgeType },
	{ id: 'e4-8', source: '4', target: '8', type: edgeType },
]

const initialNodes = [
	{
		id: '1',
		position,
		data: { label: '1' },
		style: {
			background: acceptanceColor,
			border: 'none',
		},
	},
	{
		id: '2',
		position,
		data: { label: '2' },
		style: {
			background: commentsColor,
			border: 'none',
		},
	},
	{
		id: '3',
		position,
		data: { label: '3' },
		style: {
			background: rejectionColor,
			border: 'none',
		},
	},
	{
		id: '4',
		position,
		data: { label: '4' },
		style: {
			background: acceptanceWithCommentsColor,
			border: 'none',
		},
	},
	{
		id: '5',
		position,
		data: { label: '5' },
		style: {
			background: notStartedColor,
			border: 'none',
		},
	},
	{
		id: '6',
		position,
		data: { label: '6' },
		style: {
			background: sentVerificationColor,
			border: 'none',
		},
	},
	{
		id: '7',
		position,
		data: { label: '7' },
		style: {
			background: takenVerificationColor,
			border: 'none',
		},
	},
	{
		id: '8',
		position,
		data: { label: '8' },
		style: {
			background: blockColor,
			border: 'none',
		},
	},
]

const dagreGraph = new dagre.graphlib.Graph()
dagreGraph.setDefaultEdgeLabel(() => ({}))

const nodeWidth = 172
const nodeHeight = 36

// eslint-disable-next-line @typescript-eslint/no-explicit-any
const getLayoutedElements = (nodes: any[], edges: any[], direction = 'TB') => {
	const isHorizontal = direction === 'LR'
	dagreGraph.setGraph({ rankdir: direction })

	nodes.forEach((node: { id: string }) => {
		dagreGraph.setNode(node.id, { width: nodeWidth, height: nodeHeight })
	})

	edges.forEach(
		(edge: {
			source: dagre.Edge
			target: string | { [key: string]: unknown } | undefined
		}) => {
			dagreGraph.setEdge(edge.source, edge.target)
		}
	)

	dagre.layout(dagreGraph)

	nodes.forEach(
		(node: {
			id: string | dagre.Label
			targetPosition: string
			sourcePosition: string
			position: { x: number; y: number }
		}) => {
			const nodeWithPosition = dagreGraph.node(node.id)
			node.targetPosition = isHorizontal ? 'left' : 'top'
			node.sourcePosition = isHorizontal ? 'right' : 'bottom'

			node.position = {
				x: nodeWithPosition.x - nodeWidth / 2,
				y: nodeWithPosition.y - nodeHeight / 2,
			}

			return node
		}
	)

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
		<Box className={styles.dataGraph}>
			<ReactFlow
				nodes={nodes}
				edges={edges}
				onNodesChange={onNodesChange}
				onEdgesChange={onEdgesChange}
				onConnect={onConnect}
			>
				<Panel position='top-right' className={styles.panel}>
					<GrayButton
						sx={{
							fontSize: {
								lg: '0,875rem',
							},
						}}
						variant='contained'
						onClick={() => onLayout('TB')}
					>
						Вертикальный вид
					</GrayButton>
					<GrayButton
						sx={{
							fontSize: {
								lg: '0,875rem',
							},
						}}
						variant='contained'
						onClick={() => onLayout('LR')}
					>
						Горизонтальный вид
					</GrayButton>
				</Panel>
				<Controls />
				<Background gap={12} size={1} />
			</ReactFlow>
		</Box>
	)
}

export default DataGraph
